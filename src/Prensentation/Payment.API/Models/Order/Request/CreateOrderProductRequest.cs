namespace Payment.API.Models.Order.Request;

public class CreateOrderProductRequest
{
    public int Id { get; set; }

    public Guid Code { get; set; }

    public string Name { get; set; }
}
