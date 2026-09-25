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
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();

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
        var createdProduct =
            await _productService.CreateProductAsync(product);

        return CreatedAtAction(
            nameof(GetProduct),
            new { id = createdProduct.Id },
            createdProduct);
    }

    [HttpPut("updateProductOP/{id}")]
    public async Task<IActionResult> UpdateProduct(int id, UpdateProductRequest request)
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
                message = "Optimistic Concurrency Conflict: The product was modified by another user. Please retrieve the latest version and try again.",
                strategy = "Optimistic"
            });
        }
    }

    [HttpPut("updateProductPM/{id}")]
    public async Task<IActionResult> UpdateProductPessimistic(int id, UpdateProductRequest request)
    {
        var updatedProduct = await _productService.UpdateProductPessimisticAsync(id, request);

        if (updatedProduct == null)
        {
            return NotFound();
        }

        return Ok(updatedProduct);
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

    [HttpPost("purchaseProductOP/{id}")]
    public async Task<IActionResult> PurchaseProductOptimistic(int id, [FromBody] PurchaseProductOptimisticRequest request)
    {
        try
        {
            var result = await _productService.PurchaseProductOptimisticAsync(id, request.RowVersion);

            if (result == null)
            {
                return NotFound(new { message = "Product not found." });
            }

            if (!result.Purchased)
            {
                return Conflict(result);
            }

            return Ok(result);
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict(new
            {
                message = "Optimistic Concurrency Conflict: The product was purchased or modified by another concurrent request. Please retrieve the latest stock and try again.",
                strategy = "Optimistic",
                conflict = true
            });
        }
    }

    [HttpPost("purchaseProductPM/{id}")]
    public async Task<IActionResult> PurchaseProductPessimistic(int id)
    {
        var result = await _productService.PurchaseProductPessimisticAsync(id);

        if (result == null)
        {
            return NotFound(new { message = "Product not found." });
        }

        if (!result.Purchased)
        {
            return Conflict(result);
        }

        return Ok(result);
    }

    [HttpPost("simulateConcurrency/{id}")]
    public async Task<IActionResult> SimulateConcurrency(
        int id,
        [FromQuery] string strategy = "optimistic",
        [FromQuery] int requests = 5)
    {
        var result = await _productService.SimulateConcurrencyAsync(id, strategy, requests);
        return Ok(result);
    }
}