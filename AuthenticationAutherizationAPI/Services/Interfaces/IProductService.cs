using AuthenticationAutherizationAPI.DTOs.Products;
using AuthenticationAutherizationAPI.Models;

namespace AuthenticationAutherizationAPI.Services.Interfaces;

public interface IProductService
{
    Task<Product?> GetProductByIdAsync(int id);

    Task<IList<Product>> GetAllProductsAsync();

    Task<Product> CreateProductAsync(CreateProductRequest product);

    Task<Product?> UpdateProductAsync(int id, UpdateProductRequest request);

    Task<Product?> UpdateProductPessimisticAsync(int id, UpdateProductRequest request);

    Task<bool> DeleteProductAsync(int id);

    Task<PurchaseProductResponse?> PurchaseProductAsync(int id);

    Task<PurchaseProductResponse?> PurchaseProductOptimisticAsync(int id, byte[] rowVersion);

    Task<PurchaseProductResponse?> PurchaseProductPessimisticAsync(int id);

    Task<ConcurrencyTestResponse> SimulateConcurrencyAsync(int id, string strategy, int concurrentRequests);
}