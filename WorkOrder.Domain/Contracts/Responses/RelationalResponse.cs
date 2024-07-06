namespace WorkOrder.Domain.Contracts.Responses;

public class RelationalResponse : BaseResponse
{
    public WorkOrderResponse WorkOrder { get; set; }

    public WorkResponse Work { get; set; }    
}
