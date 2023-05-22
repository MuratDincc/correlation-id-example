using Basket.API.Models.Basket.Response;
using CorrelationId.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[ApiController]
[Route("/api/v1/basket")]
public class BasketController : ControllerBase
{
    private readonly ILogger<BasketController> _logger;

    public BasketController(ILogger<BasketController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation($"Correlation Id: {Request.GetCorrelationId()} - Basket Service - Get Basket");

        var response = new GetBasketResponse
        {
            Code = Guid.NewGuid(),
            Items = new List<GetBasketProductResponse>
            {
                new GetBasketProductResponse
                {
                    Id = 1,
                    Code = Guid.NewGuid(),
                    Name = "iPhone 11"
                },
                new GetBasketProductResponse
                {
                    Id = 2,
                    Code = Guid.NewGuid(),
                    Name = "iPhone 12"
                },
            }
        };

        return Ok(response);
    }
}