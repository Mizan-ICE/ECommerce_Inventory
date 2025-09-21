using System.Web;
using ECommerce_Inventory.Domain;
using ECommerce_Inventory.Application.Dtos;
using ECommerce_Inventory.Application.Services;
using ECommerce_Inventory.Domain.Entity;
using ECommerce_Inventory.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_Inventory.Api.Controller;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;
    public ProductController(IProductService service)
    {
        _service = service;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _service.GetAllProductsAsync();
        if (products == null) 
        return NotFound("No Product is found");

        return Ok(new { Message = "Get all products is successfully ", products });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _service.GetProductByIdAsync(id);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Product not Exist");
        }
       
    }
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddProductDto productDto)
    {
        var product = await _service.AddProductAsync(productDto);
        if (product == null)
        return NotFound("No Product is found");

        return Ok(new { Message = "Add this product is successfully ", product });

    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductDto product)
    {
        var updatedProduct = await _service.UpdateProductAsync(id, product);
        if (updatedProduct == null)
            return NotFound("Product not exist");

        return Ok(new { Message = "Updated successfully ", updatedProduct });

    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedProduct = await _service.DeleteProductAsync(id);
        if (deletedProduct == null)
            return NotFound("Product not exist");

        return Ok(new { Message = deletedProduct });
    }

    [HttpGet("search")]
    [AllowAnonymous]
    public async Task<IActionResult> SearchProductsSimple([FromQuery] string q)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return BadRequest("Search keyword (q) is required.");
            }

            var products = await _service.SearchProductsAsync(q);

            if (!products.Any())
            {
                return Ok(new
                {
                    Message = $"No products found for search term '{q}'",
                    Products = products,
                    Count = 0,
                    SearchTerm = q
                });
            }

            return Ok(new
            {
                Message = $"Found {products.Count()} products matching '{q}'",
                Products = products,
                Count = products.Count(),
                SearchTerm = q
            });
        }
        catch (Exception ex)
        {
            return BadRequest($"Error searching products: {ex.Message}");
        }
    }
}

