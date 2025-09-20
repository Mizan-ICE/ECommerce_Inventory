using ECommerce_Inventory.Application.Dtos;
using ECommerce_Inventory.Application.Services;
using ECommerce_Inventory.Domain.Entity;
using ECommerce_Inventory.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_Inventory.Api.Controller;
[Route("api/[controller]")]
[ApiController]
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
        if (products == null) return NotFound("No Product is found");
        return Ok(new { Message = "Get all products is successfully ", products });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetProductByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddProductDto productDto)
    {
         await _service.AddProductAsync(productDto);
        if (productDto == null) return NotFound("No Product is found");
        return Ok(new { Message = "Added successfully ", productDto });
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductDto product)
    {
        if (product is null) 
        return BadRequest("Product  is required.");
        if (product.Id != 0 && product.Id != id)
            
         return BadRequest("Route id and body id must match.");

        product.Id = id;
        try
        {
            await _service.UpdateProductAsync(id, product);
            return Ok(new { Message = "Updated successfully ", product });
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Product not exist");
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteProductAsync(id);
            return Ok(new { Message = "Deleted successfully ", id });
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Product not exist");
        }
    }
}

