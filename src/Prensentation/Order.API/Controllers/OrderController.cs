using CorrelationId.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Order.API.Models.Order.Request;
using Order.API.Models.Order.Response;

namespace Order.API.Controllers;

[ApiController]
[Route("/api/v1/orders")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;

    public OrderController(ILogger<OrderController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateOrderRequest request)
    {
        _logger.LogInformation($"Correlation Id: {Request.GetCorrelationId()} - Order Service - OrderCreate");

        return Ok(new CreateOrderResponse
        {
            Code = request.Code
        });
    }
}