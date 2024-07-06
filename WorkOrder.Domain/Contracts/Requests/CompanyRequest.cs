using System.ComponentModel.DataAnnotations;

namespace WorkOrder.Domain.Contracts.Requests;

public class CompanyRequest
{
    [Required(ErrorMessage = "The 'Name' field is required")]
    [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]*$", ErrorMessage = "Use only letters in the 'Name' field")]
    public string Name { get; set; }
}
