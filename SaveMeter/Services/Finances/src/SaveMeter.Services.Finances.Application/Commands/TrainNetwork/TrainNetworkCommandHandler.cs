using Instapp.Common.Cqrs.Commands;
using MediatR;
using Microsoft.ML;
using Microsoft.ML.Data;
using SaveMeter.Services.Finances.Domain.Repositories;
using Serilog;

namespace SaveMeter.Services.Finances.Application.Commands.TrainNetwork
{
    class TrainNetworkCommandHandler : ICommandHandler<TrainNetworkCommand>
    {
        private IBankTransactionRepository _transactionRepository;
        private IBankTransactionMlContext _mlContext;

        public TrainNetworkCommandHandler(IBankTransactionRepository transactionRepository, IBankTransactionMlContext mlContxt)
        {
            _transactionRepository = transactionRepository;
            _mlContext = mlContxt;
        }

        public async Task<Unit> Handle(TrainNetworkCommand request, CancellationToken cancellationToken)
        {
            _mlContext.TrainModel(await _transactionRepository.GetAll());

            return Unit.Value;
        }


    }
}
