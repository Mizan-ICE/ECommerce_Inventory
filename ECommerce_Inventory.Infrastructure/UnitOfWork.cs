using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce_Inventory.Domain;
using ECommerce_Inventory.Domain.Repositories;

namespace ECommerce_Inventory.Infrastructure;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;   
    public UnitOfWork(AppDbContext dbContext,
        IProductRepository productRepository,
        ICategoryRepository categoryRepository)
    {
        _dbContext = dbContext;
        ProductRepository = productRepository;
        CategoryRepository = categoryRepository;
    }

    public IProductRepository ProductRepository { get; private set; } 
    public ICategoryRepository CategoryRepository { get; private set; }

    public async Task SaveAsync()
    {
         await _dbContext.SaveChangesAsync();
    }
}
