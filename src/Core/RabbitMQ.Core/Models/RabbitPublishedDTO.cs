namespace RabbitMQ.Core.Models;

public sealed class RabbitPublishedDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Event { get; set; }
}
