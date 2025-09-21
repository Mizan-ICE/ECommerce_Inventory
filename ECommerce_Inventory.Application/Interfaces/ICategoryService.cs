using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce_Inventory.Application.Dtos.CategoryDtos;

namespace ECommerce_Inventory.Domain.Interfaces;
public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
    Task<CategoryDto?> GetCategoryByIdAsync(int id);
    Task<string> AddCategoryAsync(AddCategoryDto categoryDto);
    Task<string> UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto);
    Task<string> DeleteCategoryAsync(int id);
}
