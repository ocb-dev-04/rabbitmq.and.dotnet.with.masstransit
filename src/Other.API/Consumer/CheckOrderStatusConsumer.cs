using MassTransit;
using Rabbit.MQ.Core.API.Constants;
using Rabbit.MQ.Core.MessageContract;
using Rabbit.MQ.Core.Models;

namespace Other.API.Consumer
{
    /// <summary>
    /// <see cref="CheckOrderStatusConsumer"/> consumer code
    /// </summary>
    public class CheckOrderStatusConsumer : IConsumer<CheckOrderStatus>
    {
        #region Props & Ctor
        
        private readonly ILogger<CheckOrderStatusConsumer> _logger;

        public CheckOrderStatusConsumer(ILogger<CheckOrderStatusConsumer> logger)
        {
            _logger = logger;
        }

        #endregion
        
        /// <summary>
        /// <see cref="CheckOrderStatusConsumer"/> consumer method
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<CheckOrderStatus> context)
        {
            _logger.LogWarning($"Find order with id => {context.Message.OrderId} in Other.API.Consumer");

            // emulate reuest to dbContext
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

    /// <summary>
    /// <see cref="CheckOrderStatusConsumer"/> configuration code
    /// </summary>
    class CheckOrderStatusConsumerDefinition :
        ConsumerDefinition<CheckOrderStatusConsumer>
    {
        public CheckOrderStatusConsumerDefinition()
        {
            EndpointName = ConsumersConstants.CheckOrderStatusConsumer;
            //ConcurrentMessageLimit = 8;
        }

        protected override void ConfigureConsumer(
            IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<CheckOrderStatusConsumer> consumerConfigurator)
        {
            endpointConfigurator.ConfigureConsumeTopology = true;

            // configure message retry with millisecond intervals
            endpointConfigurator.UseMessageRetry(r => r.Intervals(100, 200, 500, 800, 1000));

            // use the outbox to prevent duplicate events from being published
            endpointConfigurator.UseInMemoryOutbox();
        }
    }
}
