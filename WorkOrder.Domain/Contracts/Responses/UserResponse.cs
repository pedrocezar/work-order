namespace WorkOrder.Domain.Contracts.Responses;

public class UserResponse : BaseResponse
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public CompanyResponse Company { get; set; }
    public ProfileResponse Profile { get; set; }
}
