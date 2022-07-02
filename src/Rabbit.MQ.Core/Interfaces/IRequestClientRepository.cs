using MassTransit;

namespace Rabbit.MQ.Core.Interfaces
{
    public interface IRequestClientRepository<GRequest, GResult>
    {
        /// <summary>
        /// Use <see cref="GRequest"/> to get <see cref="GResult"/> value from mass transit
        /// </summary>
        /// <returns></returns>
        Task<Response<GResult>?> RequestData(object value);
    }
}
