namespace WorkOrder.Domain.Models;

public class UserModel : BaseModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }

    public int CompanyId { get; set; }
    public virtual CompanyModel Company { get; set; }

    public int ProfileId { get; set; }
    public virtual ProfileModel Profile { get; set; }

    public virtual ICollection<WorkOrderModel> WorkOrders { get; set; }
}
