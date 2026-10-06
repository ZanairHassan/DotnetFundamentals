using AuthenticationAutherizationAPI.DTOs.Products;
using AuthenticationAutherizationAPI.Models;
using AuthenticationAutherizationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAutherizationAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Manager")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("allProducts")]
    public async Task<IActionResult> GetAllProducts(int pageNumber = 1, int pageSize = 7)
    {
        if (pageNumber < 1)
        {
            return BadRequest("Page number must be greater than 0.");
        }

        if (pageSize < 1 || pageSize > 8)
        {
            return BadRequest("Page size must be between 1 and 8.");
        }

        var products = await _productService.GetAllProductsAsync(pageNumber, pageSize);

        return Ok(products);
    }

    [HttpGet("getProduct/{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost("createProduct")]
    public async Task<IActionResult> CreateProduct(CreateProductRequest product)
    {
        var createdProduct = await _productService.CreateProductAsync(product);
        if (createdProduct != null)
        {
            var response= new ProductResponse
            {
                Id = createdProduct.Id,
                Name = createdProduct.Name,
                Price = createdProduct.Price,
                StockQuantity = createdProduct.StockQuantity,
                TenantId = createdProduct.TenantId
            };

            return Ok(response);
        }

        return BadRequest(new
        {
            Message = "The User did not created due to invalid input."
        });
    }

    [HttpPut("updateProduct/{id}")]
    public async Task<IActionResult> UpdateProduct(
     int id,
     UpdateProductRequest request)
    {
        try
        {
            var updatedProduct = await _productService.UpdateProductAsync(id, request);

            if (updatedProduct == null)
            {
                return NotFound();
            }

            return Ok(updatedProduct);
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict(new
            {
                message = "The product was modified by another user. Please retrieve the latest version and try again."
            });
        }
    }

    [HttpDelete("deleteProduct/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var deleted = await _productService.DeleteProductAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return Content("Product Successfully deleted");
    }

    [HttpPost("purchaseProduct/{id}")]
    public async Task<IActionResult> PurchaseProduct(int id)
    {
        var result = await _productService.PurchaseProductAsync(id);

        if (result == null)
        {
            return NotFound(new
            {
                message = "Product not found."
            });
        }

        if (!result.Purchased)
        {
            return Conflict(result);
        }

        return Ok(result);
    }

    [HttpGet("eager/{id}")]
    public async Task<IActionResult> GetProductWithEagerLoading(int id)
    {
        var product = await _productService.GetProductWithTenantEagerAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet("lazy/{id}")]
    public async Task<IActionResult> GetProductWithLazyLoading(int id)
    {
        var product = await _productService.GetProductWithTenantLazyAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    #region Bulk Actions

    [HttpPost("bulkCreateProducts")]
    public async Task<IActionResult> BulkCreateProducts(IReadOnlyCollection<CreateProductRequest> requests)
    {
        if (requests.Count == 0)
        {
            return BadRequest(new
            {
                Message = "At least one product is required."
            });
        }

        try
        {
            var createdCount = await _productService.BulkCreateProductsAsync(requests);

            return Ok(new
            {
                Message = "Products created successfully.",
                CreatedProducts = createdCount
            });
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(new
            {
                Message = exception.Message
            });
        }
    }

    [HttpPut("bulkUpdatePrices")]
    public async Task<IActionResult> BulkUpdatePrices()
    {
        var affectedRows = await _productService.BulkUpdatePricesAsync();

        return Ok(new
        {
            Message = "Product prices updated successfully.",
            AffectedProducts = affectedRows
        });
    }

    [HttpDelete("bulkDeleteProducts")]
    public async Task<IActionResult> BulkDeleteProducts(BulkDeleteProductsRequest request)
    {
        if (request.ProductIds.Count == 0)
        {
            return BadRequest(new
            {
                Message = "At least one product ID is required."
            });
        }

        var deletedCount = await _productService.BulkDeleteProductsAsync(request.ProductIds);

        if (deletedCount == 0)
        {
            return NotFound(new
            {
                Message = "No matching products were found."
            });
        }

        return Ok(new
        {
            Message = "Products deleted successfully.",
            DeletedProducts = deletedCount
        });
    }

    #endregion
}