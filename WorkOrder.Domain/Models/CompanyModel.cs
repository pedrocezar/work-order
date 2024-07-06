namespace WorkOrder.Domain.Models;

public class CompanyModel : BaseModel
{
    public string Name { get; set; }

    public virtual ICollection<UserModel> Users { get; set; }
}
