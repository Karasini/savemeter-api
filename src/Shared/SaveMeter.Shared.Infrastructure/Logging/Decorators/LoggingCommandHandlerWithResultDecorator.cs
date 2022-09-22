using System;
using System.Threading;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.Extensions.Logging;
using SaveMeter.Shared.Abstractions.Commands;
using SaveMeter.Shared.Abstractions.Contexts;
using SaveMeter.Shared.Abstractions.Messaging;

namespace SaveMeter.Shared.Infrastructure.Logging.Decorators;

[Decorator]
internal sealed class LoggingCommandHandlerWithResultDecorator<T, TResult> : ICommandHandler<T, TResult> where T : class, ICommand
{
    private readonly ICommandHandler<T, TResult> _handler;
    private readonly IMessageContextProvider _messageContextProvider;
    private readonly IContext _context;
    private readonly ILogger<LoggingCommandHandlerDecorator<T>> _logger;

    public LoggingCommandHandlerWithResultDecorator(ICommandHandler<T, TResult> handler, IMessageContextProvider messageContextProvider,
        IContext context, ILogger<LoggingCommandHandlerDecorator<T>> logger)
    {
        _handler = handler;
        _messageContextProvider = messageContextProvider;
        _context = context;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TResult> HandleAsync(T command, CancellationToken cancellationToken = default)
    {
        var module = command.GetModuleName();
        var name = command.GetType().Name.Underscore();
        var messageContext = _messageContextProvider.Get(command);
        var requestId = _context.RequestId;
        var traceId = _context.TraceId;
        var userId = _context.Identity?.Id;
        var messageId = messageContext?.MessageId;
        var correlationId = messageContext?.Context.CorrelationId ?? _context.CorrelationId;
        _logger.LogInformation("Handling a command: {Name} ({Module}) [Request ID: {RequestId}, Message ID: {MessageId}, Correlation ID: {CorrelationId}, Trace ID: '{TraceId}', User ID: '{UserId}]'...",
            name, module, requestId, messageId, correlationId, traceId, userId);
        var result = await _handler.HandleAsync(command, cancellationToken);
        _logger.LogInformation("Handled a command: {Name} ({Module}) [Request ID: {RequestId}, Message ID: {MessageId}, Correlation ID: {CorrelationId}, Trace ID: '{TraceId}', User ID: '{UserId}']",
            name, module, requestId, messageId, correlationId, traceId, userId);

        return result;
    }
}