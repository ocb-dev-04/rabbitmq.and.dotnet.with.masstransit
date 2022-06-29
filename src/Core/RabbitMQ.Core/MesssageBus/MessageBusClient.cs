using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabbitMQ.Core.MesssageBus;

public sealed class MessageBusClient : IMessageBusClient
{
    #region Props & Ctor

    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MessageBusClient()
    {
        var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };

        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

            Console.WriteLine("--> Conected to MessageBus");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Some error ocurred => {ex.Message}");
        }
    }

    #endregion

    /// <inheritdoc/>
    public void PublishMessageBus(RabbitPublishedDTO publish)
    {

        if (_connection.IsOpen)
        {
            Console.WriteLine("->> RabbitMQ Connection Open, sending message");
            var message = JsonSerializer.Serialize(publish);
            SendMessage(message);
        }
        else
        {
            Console.WriteLine("->> RabbitMQ Connection Closed, not message");

        }
    }

    private void SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "trigger", routingKey: "", body: body);
        Console.WriteLine($"--> We have sent {message}");
    }

    public void Dispose()
    {
        Console.WriteLine("MessageBus disposed");
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }

    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
    {
        Console.WriteLine("RabbitMQ Connection Shutdown");
    }

}
