namespace WorkOrder.Domain.Contracts.Requests;

public class UserRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }

    public int CompanyId { get; set; }
    public int ProfileId { get; set; }
}
