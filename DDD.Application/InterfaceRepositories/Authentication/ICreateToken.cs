using DDD.Domain.Identity;

namespace DDD.Application.InterfaceRepositories.Authentication
{
    public interface ICreateToken
    {
       Task<string> CreateTokenAsync(ApplicationUser user);
    }
}
