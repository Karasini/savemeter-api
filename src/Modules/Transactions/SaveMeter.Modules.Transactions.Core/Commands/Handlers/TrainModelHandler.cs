using System.Threading;
using System.Threading.Tasks;
using SaveMeter.Modules.Transactions.Core.Entities;
using SaveMeter.Modules.Transactions.Core.Repositories;
using SaveMeter.Shared.Abstractions.Commands;

namespace SaveMeter.Modules.Transactions.Core.Commands.Handlers;

internal class TrainModelHandler : ICommandHandler<TrainModel>
{
    private readonly IBankTransactionRepository _transactionRepository;
    private readonly IBankTransactionMlContext _mlContext;
    private readonly IPredictionModelRepository _predictionModelRepository;

    public TrainModelHandler(IBankTransactionRepository transactionRepository, IBankTransactionMlContext mlContext,
        IPredictionModelRepository predictionModelRepository)
    {
        _transactionRepository = transactionRepository;
        _mlContext = mlContext;
        _predictionModelRepository = predictionModelRepository;
    }

    public async Task HandleAsync(TrainModel command, CancellationToken cancellationToken = default)
    {
        var model = _mlContext.TrainModel(await _transactionRepository.GetByUserId(command.UserId));

        var predictionModel = await _predictionModelRepository.GetByUserId(command.UserId) ??
                              new PredictionModel(model, command.UserId);

        _predictionModelRepository.Update(predictionModel);
    }
}