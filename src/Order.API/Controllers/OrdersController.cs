using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

using MassTransit;
using Rabbit.MQ.Core.MessageContract;
using System.Transactions;

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
            Response<OrderStatusResult>? response 
                = await _client.GetResponse<OrderStatusResult>(
                    new { OrderId = id },
                    context =>
                    {
                        context.TimeToLive = TimeSpan.FromSeconds(10);
                        context.UseTransaction(config => config.IsolationLevel = IsolationLevel.Serializable);
                        
                        _logger.LogWarning($"Request id => {context.RequestId}");
                    }, 
                    timeout: TimeSpan.FromSeconds(10));
            
            return Ok(response.Message);
        }
    }
}
