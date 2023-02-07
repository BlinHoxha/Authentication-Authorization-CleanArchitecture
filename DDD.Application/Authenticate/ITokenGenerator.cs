using DDD.Application.Authenticate;
using DDD.Domain.Identity;

namespace DDD.Application.AuthenticateInterface
{
    public interface ITokenGenerator
    {
       Task<TokenData> GenerateJwtToken(ApplicationUser user);
    }
}
