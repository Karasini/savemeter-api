using SaveMeter.Shared.Abstractions.Commands;
using System.Threading;
using System.Threading.Tasks;
using SaveMeter.Modules.Transactions.Core.DTO;
using SaveMeter.Modules.Transactions.Core.Entities;
using SaveMeter.Modules.Transactions.Core.Exceptions;
using SaveMeter.Modules.Transactions.Core.Repositories;
using SaveMeter.Shared.Abstractions.Guards;

namespace SaveMeter.Modules.Transactions.Core.Commands.Handlers;
internal class CreateTransactionHandler : ICommandHandler<CreateTransaction, BankTransactionDto>
{
    private readonly IBankTransactionRepository _transactionRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IBankTransactionMlContext _mlContext;

    public CreateTransactionHandler(IBankTransactionRepository transactionRepository, IBankTransactionMlContext mlContext, ICategoryRepository categoryRepository)
    {
        _transactionRepository = transactionRepository;
        _mlContext = mlContext;
        _categoryRepository = categoryRepository;
    }

    public async Task<BankTransactionDto> HandleAsync(CreateTransaction command, CancellationToken cancellationToken = default)
    {
        var transactionExists = await _transactionRepository.TransactionExists(command.TransactionDateUtc, command.Value);
        Guard.Against(transactionExists, new BankTransactionAlreadyExistsException(command.TransactionDateUtc, command.Value));
        //TODO? Validate user ?
        
        var categoryId = _mlContext.Predicate(command.Customer, command.Description);

        var transaction = new BankTransaction()
        {
            Value = command.Value,
            Customer = command.Customer,
            Description = command.Description,
            TransactionDate = command.TransactionDateUtc,
            CategoryId = categoryId,
            BankName = command.BankName,
            UserId = command.UserId
        };
        _transactionRepository.Add(transaction);

        var category = await _categoryRepository.GetByIdAsync(categoryId);

        return new BankTransactionDto
        {
            CategoryId = transaction.CategoryId,
            Customer = transaction.Customer,
            Description = transaction.Description,
            Id = transaction.Id,
            Value = transaction.Value,
            TransactionDate = transaction.TransactionDate,
            CategoryName = category.Name
        };
    }
}
