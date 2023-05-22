namespace Payment.API.Models.Order.Request;

public class CreateOrderRequest
{
    public Guid Code { get; set; }

    public decimal Total { get; set; }

    public List<CreateOrderProductRequest> Items { get; set; }
}
