//using API.Controllers;
//using Application.Interfaces;
//using Domain.Models;
//using FluentAssertions;
//using Microsoft.AspNetCore.Mvc;
//using Moq;

//namespace API.Tests.ProductCatalog
//{
//    public class ProductCatalogTest
//    {
//        private static readonly List<Product> products = new()
//        {
//            new Product
//            {
//                Id = Guid.NewGuid(),
//                Sku = "ABC",
//                Name = "Foco blanco",
//                Description = "Foco blanco",
//                Cost = 15.00,
//                Category = "Electornica",
//                NumberInStock = 1
//            },
//            new Product
//            {
//                Id = Guid.NewGuid(),
//                Sku = "DEF",
//                Name = "Tierra para macetas",
//                Description = "Tierra para macetas",
//                Cost = 60.00,
//                Category = "Jardineria",
//                NumberInStock = 2
//            },
//            new Product
//            {
//                Id = Guid.NewGuid(),
//                Sku = "GIH",
//                Name = "Taladro",
//                Description = "Taladro",
//                Cost = 1315.00,
//                Category = "Ferreteria",
//                NumberInStock = 3
//            }
//        };


//        [Fact]
//        public void GetAllProductCatalog_HttpResponseOK_NotNull()
//        {

//            //Arrange
//            var _serviceMoq = new Mock<IProductCatalogService>();
//            var _loggerMoq = new Mock<ILogger<ProductController>>();
//            _serviceMoq.Setup(pl => pl.Get()).Returns(products);

//            var _controller = new ProductController(_serviceMoq.Object, _loggerMoq.Object);

//            //Act
//            var result = _controller.Get().Result as OkObjectResult;

//            //Assert
//            result.Should().BeOfType<OkObjectResult>();
//            result.Value.Should().BeOfType<List<Product>>();
//        }

//        [Fact]
//        public void GetAll_TypeList_NotNull()
//        {
//            //Arrange
//            var _serviceMoq = new Mock<IProductCatalogService>();
//            var _loggerMoq = new Mock<ILogger<ProductController>>();
//            _serviceMoq.Setup(pl => pl.Get()).Returns(products);

//            var _controller = new ProductController(_serviceMoq.Object, _loggerMoq.Object);

//            //Act
//            var result = _controller.Get().Result as OkObjectResult; ;

//            //Assert
//            result.Value.Should().NotBeNull();
//            result.Value.Should().BeOfType<List<Product>>();

//        }

//        [Fact]
//        public void GetById_HttpResponseOK_NotNull()
//        {
//            //Arrange
//            var serviceMoq = new Mock<IProductCatalogService>();
//            serviceMoq
//                .Setup(pl => pl.GetById(It.IsAny<Guid>()))
//                .Returns(products[0]);
//            var _loggerMoq = new Mock<ILogger<ProductController>>();
//            var controller = new ProductController(serviceMoq.Object, _loggerMoq.Object);

//            //Act
//            var result = controller.GetById(Guid.NewGuid()).Result as OkObjectResult;

//            //Assert
//            result.Should().BeOfType<OkObjectResult>();
//            result.Value.Should().BeOfType<Product>();
//        }

//        [Fact]
//        public void GetById_IfIdExist_NotNull()
//        {
//            //Arrange
//            var serviceMoq = new Mock<IProductCatalogService>();
//            serviceMoq
//                .Setup(pl => pl.GetById(It.IsAny<Guid>()))
//                .Returns(products[0]);
//            var _loggerMoq = new Mock<ILogger<ProductController>>();
//            var controller = new ProductController(serviceMoq.Object, _loggerMoq.Object);

//            //Act
//            var result = controller.GetById(Guid.NewGuid()).Result as OkObjectResult;

//            result.Should().NotBeNull();
//            result.Value.Should().BeOfType<Product>();

//            var product = (Product)result.Value;

//            product.Id.Should().Be(products.First().Id);
//        }

//        [Fact]
//        public void GetBySku_BeOfTypeProduct_NotNull()
//        {
//            //Arrange
//            var serviceMoq = new Mock<IProductCatalogService>();
//            serviceMoq
//                .Setup(pl => pl.GetBySku(It.IsAny<string>()))
//                .Returns(products[0]);
//            var _loggerMoq = new Mock<ILogger<ProductController>>();
//            var controller = new ProductController(serviceMoq.Object, _loggerMoq.Object);

//            //Act
//            var result = controller.GetBySku("ABC").Result as OkObjectResult;

//            result.Should().NotBeNull();
//            result.Value.Should().BeOfType<Product>();

//        }

//        [Fact]
//        public void GetBySku_Exist_SameAsParameterId()
//        {
//            //Arrange
//            var serviceMoq = new Mock<IProductCatalogService>();
//            serviceMoq
//                .Setup(pl => pl.GetBySku(It.IsAny<string>()))
//                .Returns(products[0]);
//            var _loggerMoq = new Mock<ILogger<ProductController>>();
//            var controller = new ProductController(serviceMoq.Object, _loggerMoq.Object);

//            //Act
//            var result = controller.GetBySku("ABC").Result as OkObjectResult;

//            var product = (Product)result.Value;

//            product.Sku.Should().Be(products.First().Sku);
//        }

//        [Fact]
//        public void PostSearch_OkObjectResult_ObjectList()
//        {
//            //Arrange

//            var serviceMoq = new Mock<IProductCatalogService>();
//            serviceMoq
//                .Setup(pl => pl.Search(It.IsAny<Product>()))
//                .Returns(products);
//            var _loggerMoq = new Mock<ILogger<ProductController>>();
//            var controller = new ProductController(serviceMoq.Object, _loggerMoq.Object);
//            var parameters = products[0];

//            //Act
//            var result = controller.Search(parameters).Result as OkObjectResult;

//            //Assert
//            result.Should().BeOfType<OkObjectResult>();
//            result.Value.Should().BeOfType<List<Product>>();
//        }

//        [Fact]
//        public void PostSearch_ProductList_NotBeNull()
//        {
//            //Arrange

//            var serviceMoq = new Mock<IProductCatalogService>();
//            serviceMoq
//            .Setup(pl => pl.Search(It.IsAny<Product>()))
//                .Returns(products);
//            var _loggerMoq = new Mock<ILogger<ProductController>>();
//            var controller = new ProductController(serviceMoq.Object, _loggerMoq.Object);
//            var parameters = products[0];

//            //Act
//            var result = controller.Search(parameters).Result as OkObjectResult;

//            //Assert
//            result.Value.Should().NotBeNull();
//            result.Value.Should().BeOfType<List<Product>>();
//        }




//    }

//}
