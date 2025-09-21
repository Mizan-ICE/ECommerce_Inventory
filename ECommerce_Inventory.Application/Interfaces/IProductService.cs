using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce_Inventory.Application.Dtos;
using ECommerce_Inventory.Domain.Entity;

namespace ECommerce_Inventory.Domain.Interfaces;
public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<IEnumerable<ProductDto>> SearchProductsAsync(string keyword);
    Task<AddProductDto> AddProductAsync(AddProductDto productDto);
    Task<UpdateProductDto> UpdateProductAsync(int id, UpdateProductDto productDto);
    Task<string> DeleteProductAsync(int id);
}
