using Microsoft.AspNetCore.Mvc;

namespace DDD.api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BaseController : ControllerBase
{

}
