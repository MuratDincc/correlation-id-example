using CorrelationId.AspNetCore.Constants;
using CorrelationId.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Payment.API.Models.Basket.Response;
using Payment.API.Models.Order.Request;
using Payment.API.Models.Order.Response;
using Payment.API.Models.Payment.Request;
using System.Text.Json;

namespace Payment.API.Controllers;

[ApiController]
[Route("/api/v1/pay")]
public class PayController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<PayController> _logger;

    public PayController(IHttpClientFactory httpClientFactory, ILogger<PayController> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    private async Task<GetBasketResponse> GetBasket()
    {
        var httpClient = _httpClientFactory.CreateClient("Basket");

        httpClient.DefaultRequestHeaders.Add(HeaderConstants.CorrelationId, Request.GetCorrelationId());

        return await httpClient.GetFromJsonAsync<GetBasketResponse>("api/v1/basket");
    }

    private async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request)
    {
        var httpClient = _httpClientFactory.CreateClient("Order");

        httpClient.DefaultRequestHeaders.Add(HeaderConstants.CorrelationId, Request.GetCorrelationId());

        var response = await httpClient.PostAsJsonAsync<CreateOrderRequest>("api/v1/orders", request);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Correlation Id: {Request.GetCorrelationId()} - Payment Service - Order Create Error");

        string responseBody = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<CreateOrderResponse>(responseBody);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreatePaymentRequest request)
    {
        _logger.LogInformation($"Correlation Id: {Request.GetCorrelationId()} - Payment Service - Pay - Start");

        // TODO: Payment business

        var basket = await GetBasket();
        if (basket == null)
        {
            _logger.LogError($"Correlation Id: {Request.GetCorrelationId()} - Payment Service - Get Basket Error");
            return BadRequest();
        }

        var order = await CreateOrder(new CreateOrderRequest
        {
            Code = request.Code,
            Total = request.Total,
            Items = basket.Items.Select(x => new CreateOrderProductRequest {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name
            })
            .ToList()
        });

        _logger.LogInformation($"Correlation Id: {Request.GetCorrelationId()} - Payment Service - Pay - End");

        return Ok(order);
    }
}