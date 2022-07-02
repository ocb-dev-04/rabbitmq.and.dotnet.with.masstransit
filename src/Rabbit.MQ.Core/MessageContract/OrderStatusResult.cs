using MassTransit;

namespace Rabbit.MQ.Core.MessageContract;

[EntityName("check-order-status:result")]
public interface OrderStatusResult
{
    string OrderId { get; }
    DateTime Timestamp { get; }
    short StatusCode { get; }
    string StatusText { get; }
}
