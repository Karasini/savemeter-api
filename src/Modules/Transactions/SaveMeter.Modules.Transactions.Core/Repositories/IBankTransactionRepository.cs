﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SaveMeter.Modules.Transactions.Core.Entities;
using SaveMeter.Shared.Abstractions.DAL;

namespace SaveMeter.Modules.Transactions.Core.Repositories
{
    internal interface IBankTransactionRepository : IRepository<BankTransaction>
    {
        Task<bool> TransactionExists(DateTime transactionDate, decimal value);
        Task<IEnumerable<BankTransaction>> GetByUserId(Guid userId);
    }
}
