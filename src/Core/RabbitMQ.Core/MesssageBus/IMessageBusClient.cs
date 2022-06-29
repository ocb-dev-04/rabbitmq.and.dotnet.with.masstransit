using RabbitMQ.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.MesssageBus;

public interface IMessageBusClient
{
    /// <summary>
    /// Publish message bus
    /// </summary>
    /// <param name="publish"></param>
    void PublishMessageBus(RabbitPublishedDTO publish);
}
