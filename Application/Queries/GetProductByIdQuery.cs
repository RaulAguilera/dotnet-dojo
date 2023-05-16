using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto?>
    {
        public Guid Id { get; set; }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
    {
        private readonly IProductCatalogDbContext _context;

        public GetProductByIdQueryHandler(IProductCatalogDbContext context)
        {
            _context = context;
        }

        public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == request.Id);

            if (product != null)
            {
                return new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Category = product.Category,
                    Cost = product.Cost,
                    Description = product.Description,
                    NumberInStock = product.NumberInStock,
                    Sku = product.Sku
                };
            }

            return null;
        }


    }
}
