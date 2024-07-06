using WorkOrder.Domain.Enums;
using WorkOrder.Domain.Utils;

namespace WorkOrder.Domain.Contracts.Responses;

public class InformationResponse
{
    public StatusException Status { get; set;}
    public string Description { get { return Status.Description(); } }
    public List<string> Messages { get; set; }
    public string Details { get; set; }
}
