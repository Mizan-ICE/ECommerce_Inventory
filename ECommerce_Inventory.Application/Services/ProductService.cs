using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce_Inventory.Application.Dtos;
using ECommerce_Inventory.Domain;
using ECommerce_Inventory.Domain.Entity;
using ECommerce_Inventory.Domain.Interfaces;

namespace ECommerce_Inventory.Application.Services;
public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task AddProductAsync(AddProductDto productDto)
    {
        if (productDto.CategoryId <= 0 ||
                    await _unitOfWork.CategoryRepository.GetByIdAsync(productDto.CategoryId) is null)
            throw new KeyNotFoundException($"Category with id {productDto.CategoryId} not found.");

        var product = _mapper.Map<Product>(productDto);
        await _unitOfWork.ProductRepository.AddAsync(product);
        await _unitOfWork.SaveAsync();



    }

    public async Task DeleteProductAsync(int id)
    {
        var existing = await _unitOfWork.ProductRepository.GetByIdAsync(id);
        if (existing is null)
            throw new KeyNotFoundException($"Product with id {id} not found.");

        await _unitOfWork.ProductRepository.DeleteAsync(id);
        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _unitOfWork.ProductRepository.GetAllAsync();
       return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
        return _mapper.Map<ProductDto>(product);
    }

    public async Task UpdateProductAsync(int id, UpdateProductDto productDto)
    {
        var existing = await _unitOfWork.ProductRepository.GetByIdAsync(id);
        if (existing is null)
            throw new KeyNotFoundException($"Product with id {id} not found.");

        _mapper.Map(productDto, existing);

        await _unitOfWork.SaveAsync();
    }
}

