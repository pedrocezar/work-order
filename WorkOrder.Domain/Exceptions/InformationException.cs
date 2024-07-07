using WorkOrder.Domain.Enums;
using WorkOrder.Domain.Utils;

namespace WorkOrder.Domain.Exceptions;

public class InformationException : Exception
{
    public StatusException Status { get; }
    public List<string> Messages { get; }

    public InformationException(StatusException status, List<string> messages, Exception exception = null) : base(status.Description(), exception)
    {
        Status = status;
        Messages = messages;
    }

    public InformationException(StatusException status, string message, Exception exception = null) : base(status.Description(), exception)
    {
        Status = status;
        Messages = new List<string> { message };
    }
}
