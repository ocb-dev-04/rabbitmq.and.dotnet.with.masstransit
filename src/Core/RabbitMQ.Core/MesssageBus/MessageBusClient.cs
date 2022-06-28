using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.MesssageBus
{
    public sealed class MessageBusClient : IMessageBusClient
    {
        #region Props & Ctor
        
        private readonly IConfiguration _configuration;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            var factory = new ConnectionFactory() { HostName = "", Port = 0 };
        }

        #endregion

        /// <inheritdoc/>
        public void PublishMessageBus(RabbitPublishedDTO publish)
        {
            throw new NotImplementedException();
        }
    }
}
