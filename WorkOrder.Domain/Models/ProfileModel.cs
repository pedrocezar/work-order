namespace WorkOrder.Domain.Models;

public class ProfileModel : BaseModel
{
    public string Name { get; set; }

    public virtual ICollection<UserModel> Users { get; set; }
}
