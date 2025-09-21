using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce_Inventory.Application.Dtos;

namespace ECommerce_Inventory.Domain.Interfaces;
public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<AddProductDto> AddProductAsync(AddProductDto productDto);
    Task<UpdateProductDto> UpdateProductAsync(int id, UpdateProductDto productDto);
    Task<string> DeleteProductAsync(int id);
}
