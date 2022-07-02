using MassTransit;

namespace Rabbit.MQ.Core.Interfaces
{
    public interface IRequestClientRepository<GResult>
    {
        /// <summary>
        /// Make request to get <see cref="GResult"/> value from mass transit
        /// </summary>
        /// <returns></returns>
        Task<Response<GResult>?> RequestToMQ(object value, int timeoutSeconds = 10);
    }
}
