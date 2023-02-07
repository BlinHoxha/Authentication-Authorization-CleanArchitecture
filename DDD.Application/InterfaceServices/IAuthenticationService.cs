using DDD.Contracts.DTO.Authenticate;

namespace DDD.Application.InterfaceServices;

public interface IAuthenticationService
{
    Task<AuthResult> RegisterAsync(UserRegistrationRequestDTO userForRegistration);
    Task<AuthResult> LoginAsync(UserLoginRequestDTO loginDto);    
}
