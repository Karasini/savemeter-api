using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.Cqrs.Commands;
using Instapp.Common.MongoDb.UoW;
using MediatR;
using SaveMeter.Services.Finances.Application.Commands.CreateTransaction;
using SaveMeter.Services.Finances.Application.Exceptions;
using SaveMeter.Services.Finances.Domain.Aggregates.MoneySourceAggregate;
using SaveMeter.Services.Finances.Domain.Aggregates.Transaction;
using SaveMeter.Services.Finances.Domain.Repositories;

namespace SaveMeter.Services.Finances.Application.Commands.ImportTransactions
{
    internal class ImportTransactionsCommandHandler : ICommandHandler<ImportTransactionCommand>
    {
        private readonly IMediator _mediator;
        private readonly IMoneySourceRepository _moneySourceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ImportTransactionsCommandHandler(IMediator mediator, IMoneySourceRepository moneySourceRepository, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _moneySourceRepository = moneySourceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ImportTransactionCommand request, CancellationToken cancellationToken)
        {
            var transactions = new List<BankTransaction>();

            foreach (var transaction in request.Transactions)
            {
                transactions.Add(await CreateTransaction(transaction));
            }
            
            var transactionsByBank = transactions.Where(x => x != null).GroupBy(x => x.BankName);
            foreach (var transactionGroup in transactionsByBank)
            {
                var moneySourceType = GetMoneySourceType(transactionGroup.Key);
                if (moneySourceType == null) continue;

                var moneySource = await GetMoneySource((MoneySourceType) moneySourceType);

                var sum = transactionGroup.Where(x => x.TransactionDate > moneySource.StatusDate).Sum(x => x.Value);
                moneySource.UpdateNativeAmount(sum);
                _moneySourceRepository.Update(moneySource);
            }

            return Unit.Value;
        }

        private async Task<MoneySource> GetMoneySource(MoneySourceType moneySourceType)
        {
            var moneySource = await _moneySourceRepository.GetByType(moneySourceType);
            if (moneySource != null) return moneySource;

            moneySource = _moneySourceRepository.Add(new MoneySource()
            {
                StatusDate = DateTime.UtcNow,
                NativeCurrency = "PLN",
                Currency = "PLN",
                Title = moneySourceType.ToString(),
                Type = moneySourceType
            });
            await _unitOfWork.Commit();

            return moneySource;
        }

        private async Task<BankTransaction?> CreateTransaction(ImportTransactionCommand.Transaction transaction)
        {
            var transactionCommand = new CreateTransactionCommand
            {
                AccountBalance = transaction.AccountBalance,
                AccountNumber = transaction.AccountNumber,
                BankName = transaction.BankName,
                Customer = transaction.Customer,
                Description = transaction.Description,
                RelatedAccountNumber = transaction.RelatedAccountNumber,
                TransactionDateUtc = transaction.TransactionDateUtc,
                Value = transaction.Value
            };

            try
            {
                return await _mediator.Send(transactionCommand);
            }
            catch (BankTransactionAlreadyExistsException)
            {
                return null;
            }
        }

        private MoneySourceType? GetMoneySourceType(string bankType)
        {
            return bankType switch
            {
                "Millennium" => MoneySourceType.Millennium,
                "Ing" => MoneySourceType.Ing,
                _ => null
            };
        }
    }
}
