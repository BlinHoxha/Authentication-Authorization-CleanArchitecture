
using DDD.Contracts.DTO.Authenticate.Incoming;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Dotnet6MvcLogin.Controllers;

public class UserAuthenticationController : Controller
{
    public ActionResult Index()
    {
        return View();
    }

}
