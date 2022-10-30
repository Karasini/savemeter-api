using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using SaveMeter.Modules.Transactions.Core.Entities;
using SaveMeter.Modules.Transactions.Core.Repositories;
using SaveMeter.Shared.Infrastructure;
using SaveMeter.Shared.Infrastructure.Mongo.UoW;
using SaveMeter.Shared.Infrastructure.Serialization;

namespace SaveMeter.Modules.Transactions.Core.DAL;

internal class TransactionsInitializer : IInitializer
{
    class BankTransactionCsv
    {
        public DateTime TransactionDate { get; set; }
        public string Customer { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string CategoryId { get; set; }
        public string Categories { get; set; }
        public bool SkipAnalysis { get; set; }
        public string BankName { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string _id { get; set; }
        public string _t { get; set; }
    }
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBankTransactionRepository _repository;
    private readonly IJsonSerializer _jsonSerializer;

    public TransactionsInitializer(IBankTransactionRepository repository, IUnitOfWork unitOfWork, IJsonSerializer jsonSerializer)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _jsonSerializer = jsonSerializer;
    }

    public async Task InitAsync()
    {
        var userId = new Guid("2ca9a7fc-9fd1-457f-978b-1cec3fc2ed54");
        if (userId == Guid.Empty) return;

        if (await _repository.AnyAsync())
        {
            return;
        }
        
        List<BankTransactionCsv> records;
        using (var reader = new StreamReader("BankTransactionCsv.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            records = csv.GetRecords<BankTransactionCsv>().ToList();

        foreach (var record in records)
        {
            var bankName = record.BankName;
            var categoryId = new Guid(Convert.FromBase64String(record.CategoryId));
            var customer = record.Customer;
            var description = record.Description;
            var transactionDate = Convert.ToDateTime(record.TransactionDate).ToUniversalTime();
            var value = record.Value;

            var bankTransaction = new BankTransaction()
            {
                Customer = customer,
                Description = description,
                Value = value,
                BankName = bankName,
                TransactionDate = transactionDate,
                UserId = userId,
                CategoryId = categoryId
            };
            
            _repository.Add(bankTransaction);
        }

        await _unitOfWork.Commit();
    }
}