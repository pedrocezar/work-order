namespace WorkOrder.Domain.Models;

public class WorkModel : BaseModel
{
    public string Name { get; set; }

    public virtual ICollection<RelationalModel> Relationals { get; set; }
}
