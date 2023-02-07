using DDD.api.Controllers.v1;
using DDD.Contracts.DTO.Authenticate.Incoming;
using DDD.Domain.User;
using DDD.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Diagnostics;
using System.Net;

using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DDD.MVC.Controllers
{
    public class HomeController : Controller
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        UserRegistrationRequestDTO user = new UserRegistrationRequestDTO();
        List<UserRegistrationRequestDTO> users = new List<UserRegistrationRequestDTO>();

        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };

            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult Profile()
        {
            UserRegistrationRequestDTO user = JsonConvert.DeserializeObject<UserRegistrationRequestDTO>(Convert.ToString(TempData["Profile"]));
            return View(user);
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            UserLoginRequestDTO user = new UserLoginRequestDTO();
            return View(user);
        }

        public ActionResult Registration()
        {
            UserRegistrationRequestDTO user = new UserRegistrationRequestDTO();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(UserRegistrationRequestDTO user)
        {
            if (ModelState.IsValid)
            {
                user = new UserRegistrationRequestDTO();
                //List<UserRegistrationRequestDTO> list = new List<UserRegistrationRequestDTO>();
                using (HttpClient httpClient = new HttpClient(_clientHandler))
                {
                    //ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                    string endpoint = "http://localhost:44314//api/v1/Authentication/RegisterUser/";
                    using (var response = await httpClient.PostAsync(endpoint, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<UserRegistrationRequestDTO>(apiResponse);

                    }
                }
                return View(user);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestDTO user)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            using (HttpClient httpClient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                string endpoint = "https://localhost:44314//api/v1/Authentication/Login/";
                using (var response = await httpClient.PostAsync(endpoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        TempData["Profile"] = JsonConvert.SerializeObject(user);
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Username or Password is Incorrect");
                        return View();
                    }

                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string endpoint = "https://localhost:44314/Authentication/v1/logout";
                using (var response = await httpClient.PostAsync(endpoint, null))
                {
                    string logoutResult = response.Content.ReadAsStringAsync().Result;

                    logoutResult = JsonConvert.DeserializeObject<string>(logoutResult);

                    TempData["Profile"] = logoutResult;

                    return View();

                }
            }

        }



        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}