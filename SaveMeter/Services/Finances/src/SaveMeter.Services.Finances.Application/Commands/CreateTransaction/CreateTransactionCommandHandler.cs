using Instapp.Common.Cqrs.Commands;
using MediatR;
using SaveMeter.Services.Finances.Domain.Aggregates.Transaction;
using SaveMeter.Services.Finances.Domain.Repositories;

namespace SaveMeter.Services.Finances.Application.Commands.CreateTransaction
{
    class CreateTransactionCommandHandler : ICommandHandler<CreateTransactionCommand>
    {
        private readonly IBankTransactionRepository _transactionRepository;
        private readonly IBankTransactionMlContext _mlContext;

        public CreateTransactionCommandHandler(IBankTransactionRepository transactionRepository, ICategoryReferenceRepository categoryReferenceRepository, IBankTransactionMlContext mlContext, ICategoryRepository categoryRepository)
        {
            _transactionRepository = transactionRepository;
            _mlContext = mlContext;
        }

        public async Task<Unit> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            if (await _transactionRepository.TransactionExists(request.TransactionDateUtc, request.Value))
                return Unit.Value;

            var categoryId = _mlContext.Predicate(request.Customer, request.Description);

            var transaction = new BankTransaction()
            {
                Value = request.Value,
                Customer = request.Customer,
                Description = request.Description,
                TransactionDate = request.TransactionDateUtc,
                CategoryId = categoryId != "" ? Guid.Parse(categoryId) : null,
                BankName = request.BankName
            };
            _transactionRepository.Add(transaction);
            return Unit.Value;
        }
    }
}
