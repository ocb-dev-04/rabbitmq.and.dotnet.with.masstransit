using System.Transactions;

using Microsoft.Extensions.Logging;

using MassTransit;

using Rabbit.MQ.Core.Interfaces;

namespace Rabbit.MQ.Core.Implementations;

public sealed class RequestClientRepository<GRequest, GResult> 
    : IRequestClientRepository<GResult>
    where GRequest : class
    where GResult : class
{
    #region Props & Ctor

    private readonly IRequestClient<GRequest> _client;
    private readonly ILogger<RequestClientRepository<GRequest, GResult>> _logger;

    public RequestClientRepository(
        IRequestClient<GRequest> client,
        ILogger<RequestClientRepository<GRequest, GResult>> logger)
    {
        _client = client;
        _logger = logger;
    }

    #endregion

    /// <inheritdoc/>
    public async Task<Response<GResult>?> RequestToMQ(object value, int timeoutSeconds = 10)
        => await _client.GetResponse<GResult>(
                    value,
                    context =>
                    {
                        context.TimeToLive = TimeSpan.FromSeconds(timeoutSeconds);
                        context.UseTransaction(config => config.IsolationLevel = IsolationLevel.Serializable);
                        
                        _logger.LogWarning($"Request id => {context.RequestId}");
                    },
                    timeout: TimeSpan.FromSeconds(10));
}
