using WorkOrder.Domain.Models;
using System.Security.Claims;

namespace WorkOrder.Tests.Configs
{
    public static class ClaimConfig
    {
        public static IEnumerable<Claim> Get(int id, string name, string email, string profile)
        {
            return new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, profile)
                };
        }

        public static Claim[] Claims(this UserModel user)
        {
            return 
                [
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Profile.Name)
                ];
        }
    }
}
