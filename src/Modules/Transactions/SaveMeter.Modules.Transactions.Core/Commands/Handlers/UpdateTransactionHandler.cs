using SaveMeter.Shared.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SaveMeter.Modules.Transactions.Core.DTO;
using SaveMeter.Modules.Transactions.Core.Exceptions;
using SaveMeter.Modules.Transactions.Core.Repositories;
using SaveMeter.Shared.Abstractions.Guards;

namespace SaveMeter.Modules.Transactions.Core.Commands.Handlers;
internal class UpdateTransactionHandler : ICommandHandler<UpdateTransaction, BankTransactionDto>
{
    private readonly IBankTransactionRepository _transactionRepository;

    public UpdateTransactionHandler(IBankTransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<BankTransactionDto> HandleAsync(UpdateTransaction command, CancellationToken cancellationToken = default)
    {
        var transaction = await _transactionRepository.GetByIdAsync(command.Id);

        Guard.Against<BankTransactionNotFoundException>(transaction == null);
        //Guard.Against<CategoryNotFoundException>(!await _categoryRepository.Exists(x => x.Id == command.CategoryId));

        transaction.CategoryId = command.CategoryId;
        transaction.Customer = command.Customer;
        transaction.Description = command.Description;
        transaction.SkipAnalysis = command.SkipAnalysis;

        _transactionRepository.Update(transaction);

        return new BankTransactionDto
        {
             CategoryId = transaction.CategoryId,
             Customer = transaction.Customer,
             Description = transaction.Description,
             Id = transaction.Id,
             Value = transaction.Value,
             TransactionDate = transaction.TransactionDate
        };
    }
}
