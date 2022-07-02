using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

using MassTransit;
using Rabbit.MQ.Core.MessageContract;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        #region Props & Ctor

        private readonly IRequestClient<CheckOrderStatus> _client;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(
            IRequestClient<CheckOrderStatus> client,
            ILogger<OrdersController> logger)
        {
            _client = client;
            _logger = logger;
        }

        #endregion

        [HttpGet("{id}")]
        public async Task<IActionResult> CreateOrder([FromRoute, Required] string id)
        {
            _logger.LogWarning($"Find order with id => {id} in Order.API.Controllers");
            var response = await _client.GetResponse<OrderStatusResult>(new { OrderId = id });
            
            return Ok(response.Message);
        }
    }
}
