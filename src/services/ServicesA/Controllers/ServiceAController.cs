using Microsoft.AspNetCore.Mvc;

namespace ServicesA.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceAController : ControllerBase
{
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }
}
