using Domain.Models;

namespace Application.Queries
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string? Sku { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Cost { get; set; }
        public string? Category { get; set; }
        public int? NumberInStock { get; set; }


    }
}
