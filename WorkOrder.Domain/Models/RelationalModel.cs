namespace WorkOrder.Domain.Models;

public class RelationalModel : BaseModel
{
    public int WorkOrderId { get; set; }
    public virtual WorkOrderModel WorkOrder { get; set; }

    public int WorkId { get; set; }
    public virtual WorkModel Work { get; set; }    
}
