using DDD.Contracts.DTO.Authenticate.Incoming;

namespace DDD.MVC.Services
{
    public interface IAuthClientCrudService
    {
        Task RegisterUser(UserRegistrationRequestDTO user);
        Task LoginUser(UserLoginRequestDTO user);
    }
}
