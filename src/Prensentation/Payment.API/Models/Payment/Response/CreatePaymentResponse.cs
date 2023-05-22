namespace Payment.API.Models.Payment.Response;

public class CreatePaymentResponse
{
    public Guid Code { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}
