using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace NZWaks.API.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            //create claims from the roles and other info

            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));

            }
        }
    }
}
