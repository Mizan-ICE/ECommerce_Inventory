using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce_Inventory.Domain.Entity;
using ECommerce_Inventory.Domain.Repositories;

namespace ECommerce_Inventory.Infrastructure.Repositories;
public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    private readonly AppDbContext _dbcontext;
    public CategoryRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbcontext = dbContext;
    }
}
