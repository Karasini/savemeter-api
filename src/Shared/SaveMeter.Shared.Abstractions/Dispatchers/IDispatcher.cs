using System.Threading;
using System.Threading.Tasks;
using SaveMeter.Shared.Abstractions.Commands;
using SaveMeter.Shared.Abstractions.Events;
using SaveMeter.Shared.Abstractions.Queries;

namespace SaveMeter.Shared.Abstractions.Dispatchers;

public interface IDispatcher
{
    Task SendAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand;
    Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent;
    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}