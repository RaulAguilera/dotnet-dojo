using Application.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Commands
{
    public class CreateProductCommand : IRequest<Guid> 
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Cost { get; set; }
        public string Category { get; set; }
        public int NumberInStock { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductCatalogDbContext _context;

        public CreateProductCommandHandler(IProductCatalogDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Sku = request.Sku,
                Name = request.Name,
                Description = request.Description,
                Cost = request.Cost,
                Category = request.Category,
                NumberInStock = request.NumberInStock,
            };

            _context.Products.Add(product);

            await _context.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
