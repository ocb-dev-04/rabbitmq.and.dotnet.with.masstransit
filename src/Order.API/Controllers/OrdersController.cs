using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

using MassTransit;

using Rabbit.MQ.Core.MessageContract;
using Rabbit.MQ.Core.Implementations;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        #region Props & Ctor

        private readonly IRequestClient<CheckOrderStatus> _client;
        private readonly RequestClientRepository<CheckOrderStatus, OrderStatusResult> _requestClientRepository;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(
            IRequestClient<CheckOrderStatus> client, 
            RequestClientRepository<CheckOrderStatus, OrderStatusResult> requestClientRepository, 
            ILogger<OrdersController> logger)
        {
            _client = client;
            _requestClientRepository = requestClientRepository;
            _logger = logger;
        }



        #endregion

        [HttpGet("{id}")]
        public async Task<IActionResult> CreateOrder([FromRoute, Required] string id)
        {
            _logger.LogInformation($"--> Find order with id = {id} in Order.API.Controllers");
            
            Response<OrderStatusResult>? response = await _requestClientRepository.RequestToMQ(new { OrderId = id }, 20);
            if (response is null) return NotFound();

            return Ok(response.Message);
        }
    }
}
