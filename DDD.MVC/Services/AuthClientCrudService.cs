using DDD.Contracts.DTO.Authenticate.Incoming;

namespace DDD.MVC.Services
{
    public class AuthClientCrudService : IAuthClientCrudService
    {
        private static readonly HttpClient _httpclient = new HttpClient();

        public AuthClientCrudService()
        {
            _httpclient.BaseAddress = new Uri("https://localhost:44314//api/v1/");
            _httpclient.Timeout = new TimeSpan(0, 0, 30);
        }

        public Task LoginUser(UserLoginRequestDTO user)
        {
            throw new NotImplementedException();
        }

        public Task RegisterUser(UserRegistrationRequestDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
