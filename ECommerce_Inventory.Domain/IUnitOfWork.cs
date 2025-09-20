using ECommerce_Inventory.Domain.Repositories;

namespace ECommerce_Inventory.Domain
{
    
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        Task SaveAsync();
    }
}
