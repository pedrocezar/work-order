using System.ComponentModel;
using System.Reflection;

namespace WorkOrder.Domain.Utils;

public static class EnumUtil
{
    public static string Description(this Enum value)
    {
        return value.GetType()
               .GetMember(value.ToString())
               .First()
               .GetCustomAttribute<DescriptionAttribute>()?
               .Description ?? string.Empty;
    }
}
