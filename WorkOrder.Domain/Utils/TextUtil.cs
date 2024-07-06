using WorkOrder.Domain.Enums;

namespace WorkOrder.Domain.Utils;

public static class TextUtil
{
    public static int GetStatusCode(this StatusException status)
    {
        switch (status)
        {
            case StatusException.IncorrectFormat:
            case StatusException.Mandatory:
                return 400;
            case StatusException.NotAuthorized:
                return 401;
            case StatusException.ProhibitedAccess:
                return 403;
            case StatusException.NotFound:
                return 404;
            case StatusException.Unprocessed:
                return 422;
            default:
                return 500;
        }
    }

    public static int? ToInt(this string value)
    {
        int result;
        if (int.TryParse(value, out result))
            return result;
        return null;
    }
}
