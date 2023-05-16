using API.Filters;
using Application.Commands;
using Application.Interfaces;
using Application.Queries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    [ApiController]
    [ApiExceptionFilter]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductCatalogService _productCatalogService;
        private readonly Application.Interfaces.ILogger<ProductController> _logger;
        private readonly ISender? _mediator;

        public ProductController(IProductCatalogService productCatalogService, Application.Interfaces.ILogger<ProductController> logger, ISender mediator)
        {
            _productCatalogService = productCatalogService;
            _logger = logger;
            _mediator = mediator;
            _logger.LogInfo($"Logger injected into {typeof(ProductController).Name}");
        }

        /// <summary>
        /// Gets the full list of available products in the catalog
        /// </summary>
        /// <returns>List of products</returns>
        /// <remarks>
        ///     Sample request:
        ///     
        ///     GET /products
        /// </remarks>
        /// <response code="200">Returns list of products</response>
        /// <response code="500">Error processing the request</response>
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            _logger.LogDebug($"{nameof(ProductController)}, {nameof(Get)}");
            var products = await _mediator.Send(new GetProductsQuery());
            return new OkObjectResult(products);
        }

        /// <summary>
        /// Gets a single product with the specified id
        /// </summary>
        /// <param name="id">The id of the product to retrieve</param>
        /// <returns>Product</returns>
        /// <remarks>
        ///     Sample request:
        ///     
        ///     GET /products/{guid}
        /// </remarks>
        /// <response code="200">Returns product with matching id</response>
        /// <response code="500">Error processing the request</response>
        [HttpGet("{guid}")]
        public async Task<ActionResult<Product>> GetById(Guid guid)
        {
            _logger.LogDebug($"{nameof(ProductController)}, {nameof(GetById)}, {guid}");
            var p = await _mediator!.Send(new GetProductByIdQuery { Id = guid });
            return new OkObjectResult(p);
        }

        /// <summary>
        /// Gets a single product with the specified sku
        /// </summary>
        /// <param name="sku">The sku of the product to retrieve</param>
        /// <returns>Product</returns>
        /// <remarks>
        ///     Sample request:
        ///     
        ///     GET /products/sku/{sku}
        /// </remarks>
        /// <response code="200">Returns product with matching sku</response>
        /// <response code="500">Error processing the request</response>
        [HttpGet("sku/{sku}")]
        public ActionResult<Product> GetBySku(string sku)
        {
            _logger.LogDebug($"{nameof(ProductController)}, {nameof(GetBySku)}, {sku}");
            Product p = _productCatalogService.GetBySku(sku);
            return new OkObjectResult(p);
        }

        /// <summary>
        /// Gets a list of products matching the filter criteria
        /// </summary>
        /// <param name="parameters">Product properties to filter by</param>
        /// <returns>Product</returns>
        /// <remarks>
        ///     Sample request:
        ///     
        ///     POST /products/search
        ///     {
        ///         "id": 0,
        ///         "sku": "string",
        ///         "name": "string",
        ///         "description": "string",
        ///         "cost": 0,
        ///         "category": "string",
        ///         "categoryId": 0,
        ///         "numberInStock": 0
        ///     }
        /// </remarks>
        /// <response code="200">Returns productS with matching filter</response>
        /// <response code="500">Error processing the request</response>
        [HttpPost("search")]
        public async Task<ActionResult<List<ProductDto>>> Search([FromBody] PostSearchQuery parameters)
        {
            _logger.LogDebug($"{nameof(ProductController)}, {nameof(Search)}, {parameters}");
            var products = await _mediator!.Send(new PostSearchQuery { Description= parameters.Description, Name = parameters.Name, Category = parameters.Category});
            return new OkObjectResult(products);
        }

        /// <summary>
        /// Adds new product to DB
        /// </summary>
        /// <param name="product">Product properties</param>
        /// <returns>Created product location</returns>
        /// <remarks>
        ///     Sample request:
        ///     
        ///     POST /products
        ///     {
        ///         "sku": "string",
        ///         "name": "string",
        ///         "description": "string",
        ///         "cost": 0.1,
        ///         "category": "string",
        ///         "categoryId": 0,
        ///         "numberInStock": 0
        ///     }
        /// </remarks>
        /// <response code="201">Product created</response>
        /// <response code="400">Validation error</response>
        /// <response code="500">Error processing the request</response>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand product)
        {
            _logger.LogDebug($"{typeof(ProductController).Name}, {nameof(Create)}, {product}");
            var id = await _mediator!.Send(product);
            return CreatedAtAction(nameof(GetById), new { guid = id }, null);
        }
    }
}
