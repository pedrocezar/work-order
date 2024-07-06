namespace WorkOrder.Domain.Contracts.Responses;

public class WorkOrderResponse : BaseResponse
{
    public string Description { get; set; }
    public DateTime StartDate { get; set; }

    public UserResponse User { get; set; }
    public FinalizationResponse Finalization { get; set; }
}
