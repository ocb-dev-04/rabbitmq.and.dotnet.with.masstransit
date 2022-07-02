using MassTransit;

using Order.API.Models;
using Order.API.MessageContract;

namespace Order.API.Consumer
{
    public class CheckOrderStatusConsumer : IConsumer<CheckOrderStatus>
    {
        #region Props & Ctor
        
        private readonly ILogger<CheckOrderStatusConsumer> _logger;

        public CheckOrderStatusConsumer(ILogger<CheckOrderStatusConsumer> logger)
        {
            _logger = logger;
        }

        #endregion
        
        public async Task Consume(ConsumeContext<CheckOrderStatus> context)
        {
            _logger.LogWarning($"Find order with id => {context.Message.OrderId}");

            OrderDTO order = new OrderDTO(){
                Timestamp = DateTime.Now,
                StatusCode = 200,
                StatusText = "Order Processed"
            };

            await context.RespondAsync<OrderStatusResult>(new
            {
                OrderId = context.Message.OrderId,
                order.Timestamp,
                order.StatusCode,
                order.StatusText
            });
        }
    }
}
