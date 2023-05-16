using Application.Commands;
using Domain.Models;
using FluentAssertions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.Tests.Commands
{
    public class CommandsTests
    {
        private Mock<ProductCatalogDbContext> _mockDbContext;

        public CommandsTests()
        {
            var mockSet = new Mock<DbSet<Product>>();
            _mockDbContext = new Mock<ProductCatalogDbContext>();
            _mockDbContext.Setup(m => m.Products).Returns(mockSet.Object);

        }

        [Fact]
        public async Task CreateProductCommandHandler_ReturnsNewProductGuid()
        {
            //Arrange
            var handler = new CreateProductCommandHandler(_mockDbContext.Object);
            var request = new CreateProductCommand()
            {
                Sku = "testSku",
                Category = "testCategory",
                Cost = 10.0,
                Description = "testDescription",
                Name = "testName",
                NumberInStock = 1
            };
            //Act
            var result = await handler.Handle(request, new CancellationToken());
            //Assert

            result.Should().NotBeEmpty();
            _mockDbContext.Verify(m => m.Products.Add(It.IsAny<Product>()), Times.Once);
            _mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}