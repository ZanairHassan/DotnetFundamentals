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
}