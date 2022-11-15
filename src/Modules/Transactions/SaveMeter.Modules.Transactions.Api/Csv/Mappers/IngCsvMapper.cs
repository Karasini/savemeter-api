using CsvHelper.Configuration;
using SaveMeter.Modules.Transactions.Core.Commands;
using System;

namespace SaveMeter.Modules.Transactions.Api.Csv
{
    internal record IngTransaction : CsvTransaction
    {
        public override string BankName => "Ing";
    }


    internal sealed class IngCsvMapper : ClassMap<IngTransaction>
    {
        public IngCsvMapper()
        {
            Map(x => x.TransactionDateUtc).Name("Data transakcji");
            Map(x => x.RelatedAccountNumber).Name("Nr rachunku");
            Map(x => x.Customer).Name("Dane kontrahenta");
            Map(x => x.Description).Name("Tytuł");
            Map(x => x.Value).Convert(args =>
            {
                var isCharge = args.Row.TryGetField<decimal>("Kwota transakcji (waluta rachunku)", out var charge);
                return isCharge ? charge / 100 : 0;
            });
            Map(x => x.AccountBalance).Name("Saldo po transakcji");
        }
    }
}
