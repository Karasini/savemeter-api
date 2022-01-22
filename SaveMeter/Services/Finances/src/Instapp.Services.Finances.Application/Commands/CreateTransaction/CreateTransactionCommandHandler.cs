using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Instapp.Common.Cqrs.Commands;
using Instapp.Services.Finances.Domain.Aggregates.Transaction;
using Instapp.Services.Finances.Domain.Repositories;
using MediatR;

namespace Instapp.Services.Finances.Application.Commands.CreateTransaction
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
