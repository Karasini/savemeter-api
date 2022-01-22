using Instapp.Common.Cqrs.Commands;
using MediatR;
using SaveMeter.Services.Finances.Domain.Aggregates.Transaction;
using SaveMeter.Services.Finances.Domain.Repositories;

namespace SaveMeter.Services.Finances.Application.Commands.CreateTransaction
{
    class CreateTransactionCommandHandler : ICommandHandler<CreateTransactionCommand>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICategoryReferenceRepository _categoryReferenceRepository;

        public CreateTransactionCommandHandler(ITransactionRepository transactionRepository, ICategoryReferenceRepository categoryReferenceRepository)
        {
            _transactionRepository = transactionRepository;
            _categoryReferenceRepository = categoryReferenceRepository;
        }

        public async Task<Unit> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            if (await _transactionRepository.TransactionExists(request.TransactionDate, request.Value))
                return Unit.Value;

            var categoryReference = await _categoryReferenceRepository.GetIfExistsIn(request.Description) ??
                                    await _categoryReferenceRepository.GetIfExistsIn(request.Customer);

            var transaction = new BankTransaction()
            {
                Value = request.Value,
                Customer = request.Customer,
                Description = request.Description,
                TransactionDate = request.TransactionDate,
                CategoryId = categoryReference?.CategoryId
            };
            _transactionRepository.Add(transaction);
            return Unit.Value;
        }
    }
}
