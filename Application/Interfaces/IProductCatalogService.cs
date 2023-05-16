using Domain.Models;

namespace Application.Interfaces
{
    public interface IProductCatalogService
    {
        List<Product> Get();

        Product GetById(Guid id);

        Product GetBySku(string sku);

        List<Product> Search(Product parameters);
    }
}
