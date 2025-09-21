using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce_Inventory.Domain.Entity;
using ECommerce_Inventory.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_Inventory.Infrastructure.Repositories;
public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    private readonly AppDbContext _dbcontext;
    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbcontext = dbContext;
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return await _dbcontext.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        var searchTerm = keyword.ToLower().Trim();

        return await _dbcontext.Products
            .Include(p => p.Category)
            .Where(p => p.Name.ToLower().Contains(searchTerm) || 
                       p.Description.ToLower().Contains(searchTerm))
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

   
}
