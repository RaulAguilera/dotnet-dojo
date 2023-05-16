using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ProductCatalogDbContext : DbContext, IProductCatalogDbContext
    {
        public virtual DbSet<Product> Products { get; set; }

        public ProductCatalogDbContext()
        {
            
        }

        public ProductCatalogDbContext(DbContextOptions<ProductCatalogDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(p =>
            {
                p.ToContainer("Products");
                p.HasPartitionKey("Id");
            });
        }
    }
}
