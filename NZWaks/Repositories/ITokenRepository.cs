using Microsoft.AspNetCore.Identity;

namespace NZWaks.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
