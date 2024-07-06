namespace WorkOrder.Domain.Contracts.Responses;

public class FinalizationResponse : BaseResponse
{
    public float Amout { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan TimeSpent { get; set; }
}
