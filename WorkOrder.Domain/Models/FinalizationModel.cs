namespace WorkOrder.Domain.Models;

public class FinalizationModel : BaseModel
{
    public float Amount { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan TimeSpent { get; set; }

    public virtual ICollection<WorkOrderModel> WorkOrders { get; set; }
}
