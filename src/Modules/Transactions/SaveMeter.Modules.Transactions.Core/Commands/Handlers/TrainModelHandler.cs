using System.Threading;
using System.Threading.Tasks;
using SaveMeter.Modules.Transactions.Core.Repositories;
using SaveMeter.Shared.Abstractions.Commands;

namespace SaveMeter.Modules.Transactions.Core.Commands.Handlers;

internal class TrainModelHandler : ICommandHandler<TrainModel>
{
    private readonly IBankTransactionRepository _transactionRepository;
    private readonly IBankTransactionMlContext _mlContext;

    public TrainModelHandler(IBankTransactionRepository transactionRepository, IBankTransactionMlContext mlContext)
    {
        _transactionRepository = transactionRepository;
        _mlContext = mlContext;
    }

    public async Task HandleAsync(TrainModel command, CancellationToken cancellationToken = default)
    {
        _mlContext.TrainModel(await _transactionRepository.GetAllAsync());
    }
}