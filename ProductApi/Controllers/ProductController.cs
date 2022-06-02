using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
    [Authorize]
    public IEnumerable<Product> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Product
        {
            Id = index,
            Name = ProductNames[Random.Shared.Next(ProductNames.Length)],
            Price = Random.Shared.Next(0, 25) * Random.Shared.NextDouble()
        })
        .ToArray();
    }
}
