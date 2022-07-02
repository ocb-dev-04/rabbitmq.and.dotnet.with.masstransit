namespace Rabbit.MQ.Core.Models;

public class OrderDTO
{
    public DateTime Timestamp { get; set; }
    public short StatusCode { get; set; }
    public string StatusText { get; set; }
}
