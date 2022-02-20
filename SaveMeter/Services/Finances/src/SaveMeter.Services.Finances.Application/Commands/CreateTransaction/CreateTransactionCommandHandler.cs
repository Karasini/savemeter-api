using Instapp.Common.Cqrs.Commands;
using MediatR;
using SaveMeter.Services.Finances.Domain.Aggregates.Transaction;
using SaveMeter.Services.Finances.Domain.Repositories;

namespace SaveMeter.Services.Finances.Application.Commands.CreateTransaction
{
    class CreateTransactionCommandHandler : ICommandHandler<CreateTransactionCommand>
    {
        private readonly IBankTransactionRepository _transactionRepository;
        private readonly ICategoryReferenceRepository _categoryReferenceRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBankTransactionMlContext _mlContext;

        public CreateTransactionCommandHandler(IBankTransactionRepository transactionRepository, ICategoryReferenceRepository categoryReferenceRepository, IBankTransactionMlContext mlContext, ICategoryRepository categoryRepository)
        {
            _transactionRepository = transactionRepository;
            _categoryReferenceRepository = categoryReferenceRepository;
            _mlContext = mlContext;
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            if (await _transactionRepository.TransactionExists(request.TransactionDate, request.Value))
                return Unit.Value;

            var categoryId = _mlContext.Predicate(request.Customer, request.Description);

            var transaction = new BankTransaction()
            {
                Value = request.Value,
                Customer = request.Customer,
                Description = request.Description,
                TransactionDate = request.TransactionDate,
                CategoryId = categoryId != "" ? Guid.Parse(categoryId) : null,
            };
            _transactionRepository.Add(transaction);
            return Unit.Value;
        }
    }
}
