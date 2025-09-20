using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public async Task AddCategoryAsync(AddCategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        await _unitOfWork.CategoryRepository.AddAsync(category);
        await _unitOfWork.SaveAsync();

    }

    public async Task DeleteCategoryAsync(int id)
    {

        var existing = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
        if (existing is null)
            throw new KeyNotFoundException($"Category with id {id} not found.");

        await _unitOfWork.CategoryRepository.DeleteAsync(id);
        await _unitOfWork.SaveAsync();
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

    public async Task UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto)
    {
        var existing = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
        if (existing is null)
            throw new KeyNotFoundException($"Category with id {id} not found.");

        _mapper.Map(categoryDto, existing);

        await _unitOfWork.SaveAsync();
    }
}
