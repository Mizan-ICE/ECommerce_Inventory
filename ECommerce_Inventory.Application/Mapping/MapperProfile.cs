using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce_Inventory.Application.Dtos;
using ECommerce_Inventory.Application.Dtos.CategoryDtos;
using ECommerce_Inventory.Domain.Entity;

namespace ECommerce_Inventory.Application.Mapping;
public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Product,AddProductDto>().ReverseMap();
        CreateMap<Product,UpdateProductDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, AddCategoryDto>().ReverseMap();
        CreateMap<Category, UpdateCategoryDto>().ReverseMap();  

    }
}
