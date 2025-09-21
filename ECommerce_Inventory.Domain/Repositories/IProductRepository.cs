using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce_Inventory.Domain.Entity;

namespace ECommerce_Inventory.Domain.Repositories;
public interface IProductRepository: IBaseRepository<Product>
{
    Task<IEnumerable<Product>> SearchProductsAsync(string keyword);
    
}
