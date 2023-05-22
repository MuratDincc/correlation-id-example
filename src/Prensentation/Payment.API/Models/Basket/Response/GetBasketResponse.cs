namespace Payment.API.Models.Basket.Response;

public class GetBasketResponse
{
    public Guid Code { get; set; }

    public List<GetBasketProductResponse> Items { get; set; }
}
