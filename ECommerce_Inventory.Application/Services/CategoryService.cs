using AutoMapper;
using ECommerce_Inventory.Application.Dtos;
using ECommerce_Inventory.Application.Dtos.CategoryDtos;
using ECommerce_Inventory.Domain;
using ECommerce_Inventory.Domain.Entity;
using ECommerce_Inventory.Domain.Interfaces;

namespace ECommerce_Inventory.Application.Services;
public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CategoryService(IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<string> AddCategoryAsync(AddCategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        var existingCategories = await _unitOfWork.CategoryRepository.GetAllAsync();
        var existingCategory = existingCategories.FirstOrDefault(p => p.Name.ToLower() == categoryDto.Name.ToLower());

        if (existingCategory is not null)
            return "Category with the same name already exists.";

        await _unitOfWork.CategoryRepository.AddAsync(category);
        await _unitOfWork.SaveAsync();
        return "Added successfully";

    }

    public async Task<string> DeleteCategoryAsync(int id)
    {
        var existing = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
        if (existing == null)
            return null;
        if (existing.Products is not null)
            return "Can not Delete this category because it has many products";
        await _unitOfWork.CategoryRepository.DeleteAsync(id);
        await _unitOfWork.SaveAsync();
        return "deleted";
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {

        var categories = await _unitOfWork.CategoryRepository.GetAllAsync(c => c.Products);

        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            ProductCount = c.Products?.Count ?? 0
        }).ToList();
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<string> UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto)
    {
        var existing = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
        if (existing is null)
            return null;

        var existingCategories = await _unitOfWork.CategoryRepository.GetAllAsync();
        var existingCategory = existingCategories.FirstOrDefault(p => p.Name.ToLower() == categoryDto.Name.ToLower());

        if (existingCategory is not null)
            return "Category with the same name already exists.";

        _mapper.Map(categoryDto, existing);

        await _unitOfWork.SaveAsync();
        return "Updated successfully";
    }
}
