using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.Cqrs.Commands;
using Instapp.Common.Exception.Guards;
using MediatR;
using SaveMeter.Services.Finances.Application.Exceptions;
using SaveMeter.Services.Finances.Domain.Aggregates.Transaction;
using SaveMeter.Services.Finances.Domain.Repositories;

namespace SaveMeter.Services.Finances.Application.Commands.UpdateTransaction
{
    internal class UpdateTransactionCommandHandler : ICommandHandler<UpdateTransactionCommand, Guid>
    {
        private readonly IBankTransactionRepository _transactionRepository;
        private readonly ICategoryRepository _categoryRepository;

        public UpdateTransactionCommandHandler(IBankTransactionRepository transactionRepository, ICategoryRepository categoryRepository)
        {
            _transactionRepository = transactionRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Guid> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetById(request.Id);

            Guard.Against<BankTransactionNotFoundException>(transaction == null);
            Guard.Against<CategoryNotFoundException>(!await _categoryRepository.Exists(x => x.Id == request.CategoryId));

            transaction.CategoryId = request.CategoryId;
            transaction.Customer = request.Customer;
            transaction.Description = request.Description;
            transaction.SkipAnalysis = request.SkipAnalysis;

            _transactionRepository.Update(transaction);

            return transaction.Id;
        }
    }
}
