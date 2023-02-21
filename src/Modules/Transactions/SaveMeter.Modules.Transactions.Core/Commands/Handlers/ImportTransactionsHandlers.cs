using System;
using System.Threading;
using System.Threading.Tasks;
using SaveMeter.Modules.Transactions.Core.Entities;
using SaveMeter.Modules.Transactions.Core.Repositories;
using SaveMeter.Shared.Abstractions.Commands;

namespace SaveMeter.Modules.Transactions.Core.Commands.Handlers;

internal class ImportTransactionsHandlers : ICommandHandler<ImportTransactions>
{

    private readonly IBankTransactionMlContext _mlContext;
    private readonly IBankTransactionRepository _transactionRepository;

    public ImportTransactionsHandlers(IBankTransactionMlContext mlContext, IBankTransactionRepository transactionRepository)
    {
        _mlContext = mlContext;
        _transactionRepository = transactionRepository;
    }

    public async Task HandleAsync(ImportTransactions command, CancellationToken cancellationToken = default)
    {
        foreach (var transaction in command.Transactions)
        {
            await SaveTransaction(transaction);
        }
    }

    private async Task SaveTransaction(ImportTransactions.Transaction command)
    {
        var transactionExists = await _transactionRepository.TransactionExists(command.TransactionDateUtc, command.Value);
        if (transactionExists) return;
        
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
    }
}