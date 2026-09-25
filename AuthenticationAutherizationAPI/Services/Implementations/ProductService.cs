using AuthenticationAutherizationAPI.Data;
using AuthenticationAutherizationAPI.DTOs.Products;
using AuthenticationAutherizationAPI.Models;
using AuthenticationAutherizationAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace AuthenticationAutherizationAPI.Services.Implementations;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;
    private readonly IServiceScopeFactory _scopeFactory;

    public ProductService(ApplicationDbContext context, IServiceScopeFactory scopeFactory)
    {
        _context = context;
        _scopeFactory = scopeFactory;
    }

    public async Task<IList<Product>> GetAllProductsAsync()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Product> CreateProductAsync(CreateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Products.Add(product);

        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<Product?> UpdateProductAsync(int id, UpdateProductRequest request)
    {
        var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

        if (existingProduct == null)
        {
            return null;
        }

        _context.Entry(existingProduct)
            .Property(x => x.RowVersion)
            .OriginalValue = request.RowVersion;

        existingProduct.Name = request.Name;
        existingProduct.Price = request.Price;
        existingProduct.StockQuantity = request.StockQuantity;
        existingProduct.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return existingProduct;
    }

    public async Task<Product?> UpdateProductPessimisticAsync(int id, UpdateProductRequest request)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var existingProduct = await _context.Products
                .FromSqlInterpolated($"""
                SELECT *
                FROM Products WITH (UPDLOCK, ROWLOCK)
                WHERE Id = {id}
                """)
                .SingleOrDefaultAsync();

            if (existingProduct == null)
            {
                await transaction.RollbackAsync();
                return null;
            }

            existingProduct.Name = request.Name;
            existingProduct.Price = request.Price;
            existingProduct.StockQuantity = request.StockQuantity;
            existingProduct.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return existingProduct;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

        if (product == null)
        {
            return false;
        }

        _context.Products.Remove(product);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<PurchaseProductResponse?> PurchaseProductAsync(int id)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var product = await _context.Products
                .FromSqlInterpolated($"""
                SELECT *
                FROM Products WITH (UPDLOCK, ROWLOCK)
                WHERE Id = {id}
                """)
                .SingleOrDefaultAsync();

            if (product == null)
            {
                await transaction.RollbackAsync();

                return null;
            }

            if (product.StockQuantity <= 0)
            {
                await transaction.RollbackAsync();

                return new PurchaseProductResponse
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    RemainingStock = product.StockQuantity,
                    Purchased = false,
                    LockStrategy = "Pessimistic",
                    RowVersion = product.RowVersion,
                    Message = "Product is out of stock."
                };
            }

            product.StockQuantity--;
            product.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return new PurchaseProductResponse
            {
                ProductId = product.Id,
                ProductName = product.Name,
                RemainingStock = product.StockQuantity,
                Purchased = true,
                LockStrategy = "Pessimistic",
                RowVersion = product.RowVersion,
                Message = "Product purchased successfully using pessimistic locking."
            };
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<PurchaseProductResponse?> PurchaseProductOptimisticAsync(int id, byte[] rowVersion)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

        if (product == null)
        {
            return null;
        }

        if (product.StockQuantity <= 0)
        {
            return new PurchaseProductResponse
            {
                ProductId = product.Id,
                ProductName = product.Name,
                RemainingStock = product.StockQuantity,
                Purchased = false,
                LockStrategy = "Optimistic",
                RowVersion = product.RowVersion,
                Message = "Product is out of stock."
            };
        }

        _context.Entry(product)
            .Property(x => x.RowVersion)
            .OriginalValue = rowVersion;

        product.StockQuantity--;
        product.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new PurchaseProductResponse
        {
            ProductId = product.Id,
            ProductName = product.Name,
            RemainingStock = product.StockQuantity,
            Purchased = true,
            LockStrategy = "Optimistic",
            RowVersion = product.RowVersion,
            Message = "Product purchased successfully using optimistic locking."
        };
    }

    public async Task<PurchaseProductResponse?> PurchaseProductPessimisticAsync(int id)
    {
        return await PurchaseProductAsync(id);
    }

    public async Task<ConcurrencyTestResponse> SimulateConcurrencyAsync(int id, string strategy, int concurrentRequests)
    {
        if (concurrentRequests <= 0) concurrentRequests = 5;
        if (concurrentRequests > 50) concurrentRequests = 50;

        var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (product == null)
        {
            return new ConcurrencyTestResponse
            {
                Strategy = strategy,
                Logs = { $"Product with ID {id} not found." }
            };
        }

        var response = new ConcurrencyTestResponse
        {
            Strategy = strategy,
            TotalRequests = concurrentRequests,
            InitialStock = product.StockQuantity
        };

        var stopwatch = Stopwatch.StartNew();
        var logs = new ConcurrentBag<string>();
        var successCount = 0;
        var conflictCount = 0;
        var outOfStockCount = 0;

        if (strategy.Equals("optimistic", StringComparison.OrdinalIgnoreCase))
        {
            var initialRowVersion = product.RowVersion;

            var tasks = Enumerable.Range(1, concurrentRequests).Select(async index =>
            {
                using var scope = _scopeFactory.CreateScope();
                var scopedContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var scopedService = new ProductService(scopedContext, _scopeFactory);

                try
                {
                    var result = await scopedService.PurchaseProductOptimisticAsync(id, initialRowVersion);
                    if (result != null && result.Purchased)
                    {
                        Interlocked.Increment(ref successCount);
                        logs.Add($"Request #{index}: Purchase SUCCEEDED (Optimistic). Remaining stock: {result.RemainingStock}");
                    }
                    else
                    {
                        Interlocked.Increment(ref outOfStockCount);
                        logs.Add($"Request #{index}: Purchase FAILED - Out of stock.");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    Interlocked.Increment(ref conflictCount);
                    logs.Add($"Request #{index}: Concurrency CONFLICT detected! RowVersion was changed by a competing transaction.");
                }
                catch (Exception ex)
                {
                    logs.Add($"Request #{index}: Error - {ex.Message}");
                }
            });

            await Task.WhenAll(tasks);
        }
        else
        {
            var tasks = Enumerable.Range(1, concurrentRequests).Select(async index =>
            {
                using var scope = _scopeFactory.CreateScope();
                var scopedContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var scopedService = new ProductService(scopedContext, _scopeFactory);

                try
                {
                    var result = await scopedService.PurchaseProductPessimisticAsync(id);
                    if (result != null && result.Purchased)
                    {
                        Interlocked.Increment(ref successCount);
                        logs.Add($"Request #{index}: Purchase SUCCEEDED (Pessimistic). Acquired lock and updated stock to {result.RemainingStock}");
                    }
                    else
                    {
                        Interlocked.Increment(ref outOfStockCount);
                        logs.Add($"Request #{index}: Purchase FAILED - Out of stock.");
                    }
                }
                catch (Exception ex)
                {
                    logs.Add($"Request #{index}: Error - {ex.Message}");
                }
            });

            await Task.WhenAll(tasks);
        }

        stopwatch.Stop();

        var finalProduct = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        response.SuccessfulPurchases = successCount;
        response.ConcurrencyConflicts = conflictCount;
        response.OutOfStockCount = outOfStockCount;
        response.FinalStock = finalProduct?.StockQuantity ?? 0;
        response.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        response.Logs = logs.OrderBy(x => x).ToList();

        return response;
    }
}