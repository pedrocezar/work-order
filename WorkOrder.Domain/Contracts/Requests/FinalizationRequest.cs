namespace WorkOrder.Domain.Contracts.Requests;

public class FinalizationRequest
{
    public float Amount { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan TimeSpent { get; set; }
}
