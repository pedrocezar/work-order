using System.ComponentModel;

namespace WorkOrder.Domain.Enums;

public enum StatusException
{
    [Description("None")]
    None = 0,

    [Description("Something unexpected happened")]
    Error = 1,

    [Description("Data not found")]
    NotFound = 2,

    [Description("Unauthorized access")]
    NotAuthorized = 3,

    [Description("Mandatory field(s) not provided")]
    Mandatory = 4,

    [Description("Field(s) with incorrect format(s)")]
    IncorrectFormat = 5,

    [Description("Unprocessed data")]
    Unprocessed = 6,

    [Description("Prohibited access")]
    ProhibitedAccess = 7
}
