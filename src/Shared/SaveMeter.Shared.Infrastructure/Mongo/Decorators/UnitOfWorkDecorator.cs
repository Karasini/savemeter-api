using System.Threading;
using System.Threading.Tasks;
using SaveMeter.Shared.Abstractions.Commands;
using SaveMeter.Shared.Abstractions.Queries;
using SaveMeter.Shared.Infrastructure.Mongo.UoW;

namespace SaveMeter.Shared.Infrastructure.Mongo.Decorators;

[Decorator]
public class UnitOfWorkDecorator<T> : ICommandHandler<T> where T : class, ICommand
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICommandHandler<T> _handler;

    public UnitOfWorkDecorator(IUnitOfWork unitOfWork, ICommandHandler<T> handler)
    {
        _unitOfWork = unitOfWork;
        _handler = handler;
    }

    public async Task HandleAsync(T command, CancellationToken cancellationToken = default)
    {
        await _handler.HandleAsync(command, cancellationToken);
        await _unitOfWork.Commit();
    }
}