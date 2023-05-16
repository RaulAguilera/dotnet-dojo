using Application.Interfaces;
using Domain.Models;


namespace Application.Services
{
    public class ProductCatalogService : IProductCatalogService
    {
        private readonly IProductCatalogDbContext _dbContext;

        public ProductCatalogService(IProductCatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> Get()
        {
            return _dbContext.Products.ToList();
        }

        public Product GetById(Guid id)
        {
            Product p = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            return p;
        }

        public Product GetBySku(string Sku)
        {
            Product p = _dbContext.Products.FirstOrDefault(p => p.Sku == Sku);
            return p;
        }

        public List<Product> Search(Product parameters)
        {
            return _dbContext.Products.ToList();
        }
    }
}
