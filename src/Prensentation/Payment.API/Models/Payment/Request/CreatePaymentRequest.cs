namespace Payment.API.Models.Payment.Request;

public class CreatePaymentRequest
{
    public Guid Code { get; set; }

    public decimal Total { get; set; }
}
