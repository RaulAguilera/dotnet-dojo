using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.Queries
{
    public class PostSearchQuery : IRequest<List<ProductDto>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }

    public class PostSearchQueryHandler : IRequestHandler<PostSearchQuery, List<ProductDto>>
    {
        private readonly IProductCatalogDbContext _context;

        public PostSearchQueryHandler(IProductCatalogDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> Handle(PostSearchQuery request, CancellationToken cancellationToken)
        {

            var query = _context.Products.Select
                (p => new ProductDto
                {
                    Id = p.Id,
                    Sku = p.Sku,
                    Name = p.Name,
                    Description = p.Description,
                    Cost = p.Cost,
                    Category = p.Category,
                    NumberInStock = p.NumberInStock
                }
                );


            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(p => p.Name.Contains(request.Name));
            }
            if (!string.IsNullOrEmpty(request.Description))
            {
                query = query.Where(p => p.Description.Contains(request.Description));
            }
            if (!string.IsNullOrEmpty(request.Category))
            {
                query = query.Where(p => p.Category.Contains(request.Category));
            }


            return await query.ToListAsync();
        }

    }
}