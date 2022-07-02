namespace Rabbit.MQ.Core.MessageContract;

public interface OrderStatusResult
{
    string OrderId { get; }
    DateTime Timestamp { get; }
    short StatusCode { get; }
    string StatusText { get; }
}
