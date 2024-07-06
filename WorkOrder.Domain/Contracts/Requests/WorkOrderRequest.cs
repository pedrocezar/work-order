namespace WorkOrder.Domain.Contracts.Requests;

public class WorkOrderRequest
{
    public string Description { get; set; }
    public DateTime StartDate { get; set; }

    public int UserId { get; set; }
    public int ServiceProviderId { get; set; }
    public int FinalizationId { get; set; }
}
