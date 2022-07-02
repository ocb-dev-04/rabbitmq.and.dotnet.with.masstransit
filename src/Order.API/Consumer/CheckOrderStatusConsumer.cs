using MassTransit;

using Order.API.Models;
using Order.API.MessageContract;

namespace Order.API.Consumer
{
    public class CheckOrderStatusConsumer :
    IConsumer<CheckOrderStatus>
    {
        private readonly ILogger<CheckOrderStatusConsumer> _logger;

        public CheckOrderStatusConsumer(ILogger<CheckOrderStatusConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CheckOrderStatus> context)
        {
            OrderDTO order = new OrderDTO(){
                Timestamp = DateTime.Now,
                StatusCode = 200,
                StatusText = "Order Processed"
            };

            await context.RespondAsync<OrderStatusResult>(new
            {
                OrderId = Guid.NewGuid().ToString(),
                order.Timestamp,
                order.StatusCode,
                order.StatusText
            });
        }
    }
}
