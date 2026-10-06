using AuthenticationAutherizationAPI.Data;
using AuthenticationAutherizationAPI.DTOs.Products;
using AuthenticationAutherizationAPI.Models;
using AuthenticationAutherizationAPI.Services.Interfaces;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AuthenticationAutherizationAPI.Services.Implementations;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IList<ProductResponse>> GetAllProductsAsync(int pageNumber, int pageSize)
    {
        var query = _context.Products
             .AsNoTracking()
             .OrderBy(p => p.Id)
             .Skip((pageNumber - 1) * pageSize)
             .Take(pageSize)
             .Select(p => new ProductResponse
             {
                 Id = p.Id,
                 Name = p.Name,
                 Price = p.Price,
                 StockQuantity = p.StockQuantity,
                 TenantId = p.TenantId
             });
        Console.WriteLine(query.ToQueryString());
        return await query.ToListAsync();
    }
    public async Task<ProductResponse?> GetProductByIdAsync(int id)
    {
        var query = _context.Products
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                TenantId = p.TenantId
            });

        Console.WriteLine(query.ToQueryString());

        return await query.FirstOrDefaultAsync();
    }

    public async Task<Product?> CreateProductAsync(CreateProductRequest request)
    {
        var tenant = await _context.Tenants.SingleOrDefaultAsync(x => x.TenantKey == request.TenantKey.Trim());

        if (tenant is null)
        {
            return null;
        }

        var product = new Product
        {
            Name = request.Name.Trim(),
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            TenantId = tenant.Id,
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

        // Tell EF Core which version the client originally retrieved.
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
                Message = "Product purchased successfully."
            };
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<ProductWithTenantResponse?> GetProductWithTenantEagerAsync(int id)
    {
        var query = _context.Products
            .Include(p => p.Tenant)
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ProductWithTenantResponse
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                TenantId = p.TenantId,
                TenantName = p.Tenant.Name,
                TenantKey = p.Tenant.TenantKey
            });

        Console.WriteLine(query.ToQueryString());

        return await query.FirstOrDefaultAsync();
    }

    public async Task<ProductWithTenantResponse?> GetProductWithTenantLazyAsync(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
        {
            return null;
        }

        var tenant = product.Tenant;

        return new ProductWithTenantResponse
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            TenantId = product.TenantId,
            TenantName = tenant.Name,
            TenantKey = tenant.TenantKey
        };
    }

    #region Bulk service actions

    public async Task<int> BulkCreateProductsAsync(IReadOnlyCollection<CreateProductRequest> requests)
    {
        if (requests.Count == 0)
        {
            return 0;
        }

        var tenantKeys = requests
            .Select(request => request.TenantKey.Trim())
            .Distinct()
            .ToList();

        var tenants = await _context.Tenants
            .Where(tenant => tenantKeys.Contains(tenant.TenantKey))
            .ToDictionaryAsync(
                tenant => tenant.TenantKey,
                tenant => tenant.Id);

        var products = new List<Product>();

        foreach (var request in requests)
        {
            var tenantKey = request.TenantKey.Trim();

            if (!tenants.TryGetValue(tenantKey, out var tenantId))
            {
                throw new InvalidOperationException($"Tenant '{tenantKey}' was not found.");
            }

            products.Add(new Product
            {
                Name = request.Name.Trim(),
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                TenantId = tenantId,
                UpdatedAt = DateTime.UtcNow
            });
        }

        await _context.Products.AddRangeAsync(products);

        await _context.SaveChangesAsync();

        return products.Count;
    }

    public async Task<int> BulkUpdatePricesAsync()
    {
        return await _context.Products
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(
                    product => product.Price,
                    product => product.Price < 500
                        ? product.Price * 1.05m
                        : product.Price < 2000
                            ? product.Price * 1.08m
                            : product.Price * 1.10m)
                .SetProperty(
                    product => product.UpdatedAt,
                    product => DateTime.UtcNow));
    }

    public async Task<int> BulkDeleteProductsAsync(IReadOnlyCollection<int> productIds)
    {
        if (productIds.Count == 0)
        {
            return 0;
        }

        return await _context.Products
            .Where(product => productIds.Contains(product.Id))
            .ExecuteDeleteAsync();
    }

    #endregion
}