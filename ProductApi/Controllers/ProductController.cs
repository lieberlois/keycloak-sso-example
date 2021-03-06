using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProductApi.Models;

namespace ProductApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private static readonly string[] ProductNames = new[]
    {
        "Hammer", "Nails", "Sink", "Drill", "Wrench"
    };

    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Authorize(Roles = "read-product")]
    public ActionResult<ProductListDto> Get()
    {
        var dto = new ProductListDto()
        {
            Products = Enumerable.Range(1, 5).Select(index => new Product
            {
                Id = index,
                Name = ProductNames[Random.Shared.Next(ProductNames.Length)],
                Price = Random.Shared.Next(0, 25) * Random.Shared.NextDouble()
            })
            .ToArray()
        };

        return dto;
    }

    [HttpGet("example")]
    [Authorize(Policy = "product#example")]
    public ActionResult<Product> GetExample()
    {
        return new Product()
        {
            Id = 1,
            Name = "Example Product",
            Price = 1234.56
        };
    }
}
