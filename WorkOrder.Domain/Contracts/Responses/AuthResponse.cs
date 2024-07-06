namespace WorkOrder.Domain.Contracts.Responses;

public class AuthResponse
{
    public string Token { get; set; }
    public DateTime? ExpirationDate { get; set; }
}
