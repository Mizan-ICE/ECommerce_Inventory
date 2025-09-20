using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ECommerce_Inventory.Domain.Entity;
using ECommerce_Inventory.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_Inventory.Infrastructure.Repositories;
public class BaseRepository<T>: IBaseRepository<T> where T : class,IEntity ,new()
{
    private readonly DbContext _dbcontext;
    public BaseRepository(DbContext dbContext)
    {
        _dbcontext = dbContext;
    }
   
    public async Task AddAsync(T entity)
    {
        await _dbcontext.Set<T>().AddAsync(entity);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbcontext.Set<T>().Update(entity);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbcontext.Set<T>().Remove(entity);
            await _dbcontext.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbcontext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbcontext.Set<T>().FindAsync(id);
    }
    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _dbcontext.Set<T>();
        query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        return await query.ToListAsync();
    }
}
