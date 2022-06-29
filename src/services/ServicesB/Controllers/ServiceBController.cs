using Microsoft.AspNetCore.Mvc;

namespace ServicesB.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceBController : ControllerBase
{
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }
}
