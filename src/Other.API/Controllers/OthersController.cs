using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Other.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OthersController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok();
    }
}
