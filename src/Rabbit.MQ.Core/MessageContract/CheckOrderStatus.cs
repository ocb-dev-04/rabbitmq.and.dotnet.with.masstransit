using MassTransit;

namespace Rabbit.MQ.Core.MessageContract;

[EntityName("check-order-status:request")]
public interface CheckOrderStatus
{
    string OrderId { get; }
}
