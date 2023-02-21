using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.Extensions.Logging;
using SaveMeter.Shared.Abstractions.Contexts;
using SaveMeter.Shared.Abstractions.Messaging;
using SaveMeter.Shared.Abstractions.Modules;
using SaveMeter.Shared.Infrastructure.Messaging.Contexts;
using SaveMeter.Shared.Infrastructure.Messaging.Dispatchers;
namespace SaveMeter.Shared.Infrastructure.Messaging.Brokers;

internal sealed class InMemoryMessageBroker : IMessageBroker
{
    private readonly IModuleClient _moduleClient;
    private readonly IAsyncMessageDispatcher _asyncMessageDispatcher;
    private readonly IContext _context;
    private readonly IMessageContextRegistry _messageContextRegistry;
    private readonly MessagingOptions _messagingOptions;
    private readonly ILogger<InMemoryMessageBroker> _logger;

    public InMemoryMessageBroker(IModuleClient moduleClient, IAsyncMessageDispatcher asyncMessageDispatcher,
        IContext context, IMessageContextRegistry messageContextRegistry,
        MessagingOptions messagingOptions,ILogger<InMemoryMessageBroker> logger)
    {
        _moduleClient = moduleClient;
        _asyncMessageDispatcher = asyncMessageDispatcher;
        _context = context;
        _messageContextRegistry = messageContextRegistry;
        _messagingOptions = messagingOptions;
        _logger = logger;
    }

    public Task PublishAsync(IMessage message, CancellationToken cancellationToken = default)
        => PublishAsync(cancellationToken, message);

    public Task PublishAsync(IMessage[] messages, CancellationToken cancellationToken = default)
        => PublishAsync(cancellationToken, messages);
        
    private async Task PublishAsync(CancellationToken cancellationToken, params IMessage[] messages)
    {
        if (messages is null)
        {
            return;
        }

        messages = messages.Where(x => x is not null).ToArray();

        if (!messages.Any())
        {
            return;
        }

        foreach (var message in messages)
        {
            var messageContext = new MessageContext(Guid.NewGuid(), _context);
            _messageContextRegistry.Set(message, messageContext);
                
            var module = message.GetModuleName();
            var name = message.GetType().Name.Underscore();
            var requestId = _context.RequestId;
            var traceId = _context.TraceId;
            var userId = _context.Identity?.Id;
            var messageId = messageContext.MessageId;
            var correlationId = messageContext.Context.CorrelationId;
                
            _logger.LogInformation("Publishing a message: {Name} ({Module}) [Request ID: {RequestId}, Message ID: {MessageId}, Correlation ID: {CorrelationId}, Trace ID: '{TraceId}', User ID: '{UserId}]...",
                name, module, requestId, messageId, correlationId, traceId, userId);
        }

        var tasks = _messagingOptions.UseAsyncDispatcher
            ? messages.Select(message => _asyncMessageDispatcher.PublishAsync(message, cancellationToken))
            : messages.Select(message => _moduleClient.PublishAsync(message, cancellationToken));

        await Task.WhenAll(tasks);
    }
}