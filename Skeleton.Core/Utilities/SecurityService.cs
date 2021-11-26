using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Skeleton.Core.Utilities
{
    public static class SecurityService
    {
        public static SecurityKey GetSymmetricSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
