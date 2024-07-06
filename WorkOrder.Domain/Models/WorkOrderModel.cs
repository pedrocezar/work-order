namespace WorkOrder.Domain.Models;

public class WorkOrderModel : BaseModel
{
    public string Description { get; set; }
    public DateTime StartDate { get; set; }

    public int UserId { get; set; }
    public virtual UserModel User { get; set; }

    public int FinalizationId { get; set; }
    public virtual FinalizationModel Finalization { get; set; }

    public virtual ICollection<RelationalModel> Relationals { get; set; }
}
