using System.Threading;
using System.Threading.Tasks;
using SaveMeter.Shared.Abstractions.Commands;
using SaveMeter.Shared.Abstractions.Queries;
using SaveMeter.Shared.Infrastructure.Mongo.UoW;

namespace SaveMeter.Shared.Infrastructure.Mongo.Decorators;

[Decorator]
internal sealed class UnitOfWorkDecoratorWithResult<TCommand, TResult> : ICommandHandler<TCommand, TResult> where TCommand : class, ICommand<TResult>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly ICommandHandler<TCommand, TResult> _handler;

    public UnitOfWorkDecoratorWithResult(IUnitOfWork unitOfWork, ICommandHandler<TCommand, TResult> handler)
    {
        _unitOfWork = unitOfWork;
        _handler = handler;
    }

    public async Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _handler.HandleAsync(command, cancellationToken);

        await _unitOfWork.Commit();

        return result;
    }
}