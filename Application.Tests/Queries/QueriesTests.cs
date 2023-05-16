using Domain.Models;
using Infrastructure.Persistence;
using Application.Queries;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;

namespace Application.Tests.Queries
{
    public class QueriesTests
    {
        private IQueryable<Product> _products;
        private Mock<ProductCatalogDbContext> _mockDbContext;

        public QueriesTests()
        {
            _products = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Sku = "ABC",
                    Name = "Foco blanco",
                    Description = "Foco blanco",
                    Cost = 15.00,
                    Category = "Electornica",
                    NumberInStock = 1
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Sku = "DEF",
                    Name = "Tierra para macetas",
                    Description = "Tierra para macetas",
                    Cost = 60.00,
                    Category = "Jardineria",
                    NumberInStock = 2
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Sku = "GIH",
                    Name = "Taladro",
                    Description = "Taladro",
                    Cost = 1315.00,
                    Category = "Ferreteria",
                    NumberInStock = 3
                }
            }.AsQueryable();
            _mockDbContext = new Mock<ProductCatalogDbContext>();
            _mockDbContext.Setup(c => c.Products).ReturnsDbSet(_products);

        }

        [Fact]
        public async Task GetProductsQueryHandler_ReturnsProductsList()
        {
            //Arrange
            var handler = new GetProductsQueryHandler(_mockDbContext.Object);
            //Act
            var result = await handler.Handle(new GetProductsQuery(), new CancellationToken());
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<ProductDto>>();
            result.Should().HaveCount(3, "We have 3 elements in our mock data");
        }

        [Fact]
        public async Task GetProductByIdQueryHandler_ReturnsCorrectProduct()
        {
            //Arrange
            var handler = new GetProductByIdQueryHandler(_mockDbContext.Object);
            var request = new GetProductByIdQuery()
            {
                Id = _products!.First().Id
            };
            //Act
            var result = await handler.Handle(request, new CancellationToken());
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ProductDto?>();
            result!.Id.Should().Be(_products!.First().Id);
        }

        [Fact]
        public async Task GetProductByIdQueryHandler_ReturnsNullWhenNoProductFound()
        {
            //Arrange
            var handler = new GetProductByIdQueryHandler(_mockDbContext.Object);
            var request = new GetProductByIdQuery()
            {
                Id = new Guid()
            };
            //Act
            var result = await handler.Handle(request, new CancellationToken());
            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task PostProductsSearchHandler_ReturnsOneOrMoreProductsFound()
        {
            //Arrange
            var handler = new PostSearchQueryHandler(_mockDbContext.Object);
            var request = new PostSearchQuery()
            {                
                
                 Description = "Foco blanco"
                
            };
            //Act
            var result = await handler.Handle(request, new CancellationToken());
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<ProductDto>>();
            result.Should().HaveCount(1, "We have 1 element in local mock data");
        }

    }
}