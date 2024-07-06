using Microsoft.AspNetCore.Http;

namespace WorkOrder.Domain.Utils;

public static class ClaimUtil
{
    public static string GetClaim(this HttpContext httpContext, string claimTypes)
    {
        var claim = httpContext?.User?.Claims;

        if (claim != null && claim.Any())
            return claim.FirstOrDefault(x => x.Type.Equals(claimTypes)).Value;

        return null;
    }
}
