namespace WorkOrder.Domain.Contracts.Requests;

public class RelationalRequest
{
    public int WorkOrderId { get; set; }

    public int WorkId { get; set; }
}
