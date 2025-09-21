using ECommerce_Inventory.Application.Dtos;
using ECommerce_Inventory.Application.Dtos.CategoryDtos;
using ECommerce_Inventory.Application.Services;
using ECommerce_Inventory.Domain.Entity;
using ECommerce_Inventory.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_Inventory.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
          var category = await _categoryService.GetCategoryByIdAsync(id);
            
         if(category==null)
         return NotFound("No category is found");
        return Ok(category);



    }
    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryDto categoryDto)
    {

        var addCategory = await _categoryService.AddCategoryAsync(categoryDto);
        if (addCategory == null)
            return NotFound("Not found Categories yet");

        return Ok(addCategory);




    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto categoryDto)
    {
        var editCategory = await _categoryService.UpdateCategoryAsync(id, categoryDto);
        if (editCategory == null)
            return NotFound("Not found Categories yet");
        return Ok(editCategory);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {

        var category = await _categoryService.DeleteCategoryAsync(id);
        if (category is null)
            return NotFound("Category Not Exist");
        return Ok(category);


    }
}
