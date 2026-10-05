using AuthenticationAutherizationAPI.DTOs.Products;
using AuthenticationAutherizationAPI.Models;

namespace AuthenticationAutherizationAPI.Services.Interfaces;

public interface IProductService
{
    Task<IList<ProductResponse>> GetAllProductsAsync(int pageNumber, int pageSize);
    Task<ProductResponse?> GetProductByIdAsync(int id);

    Task<Product?> CreateProductAsync(CreateProductRequest product);

    Task<Product?> UpdateProductAsync(int id, UpdateProductRequest request);

    Task<bool> DeleteProductAsync(int id);
    Task<PurchaseProductResponse?> PurchaseProductAsync(int id);

    Task<ProductWithTenantResponse?> GetProductWithTenantEagerAsync(int id);

    Task<ProductWithTenantResponse?> GetProductWithTenantLazyAsync(int id);
}