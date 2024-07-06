using System.ComponentModel.DataAnnotations;

namespace WorkOrder.Domain.Contracts.Requests;

public class AuthRequest
{
    [Required(ErrorMessage = "The 'Email' field is mandatory")]
    public string Email { get; set; }

    [Required(ErrorMessage = "The 'Password' field is mandatory")]
    public string Password { get; set; }
}
