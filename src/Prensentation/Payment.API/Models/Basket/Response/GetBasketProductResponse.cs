namespace Payment.API.Models.Basket.Response;

public class GetBasketProductResponse
{
    public int Id { get; set; }

    public Guid Code { get; set; }

    public string Name { get; set; }
}
