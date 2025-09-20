using ECommerce_Inventory.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_Inventory.Infrastructure;

public class AppDbContext: DbContext
{
   public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
   {
   }

   public DbSet<Product> Products { get; set; }
   public DbSet<Category> Categories { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();
        base.OnModelCreating(modelBuilder);
      
   }
}
