using Microsoft.AspNetCore.Mvc;

using RabbitMQ.Core.MesssageBus;

namespace ServicesB.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceBController : ControllerBase
{

    private readonly IMessageBusClient _messageBus;
    private readonly ILogger<ServiceBController> _logger;

    public ServiceBController(IMessageBusClient messageBus, ILogger<ServiceBController> logger)
    {
        _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost]
    public IActionResult Post([FromBody] string value)
    {
        try
        {
            _logger.LogWarning("Sending data to message bus");
            var publishMessage = new RabbitMQ.Core.Models.RabbitPublishedDTO()
            {
                Event = "trigger",
                Id = Guid.NewGuid(),
                Name = value
            };
            _messageBus.PublishMessageBus(publishMessage);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message);
        }
    }
}
