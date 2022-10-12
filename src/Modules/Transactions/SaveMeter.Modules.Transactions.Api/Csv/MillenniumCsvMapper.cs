using System;
using System.Text.RegularExpressions;
using CsvHelper.Configuration;

namespace SaveMeter.Modules.Transactions.Api.Csv
{
    internal record MillenniumTransaction : CsvTransaction
    {
        public override string BankName => "Millennium";
    }

    internal sealed class MillenniumCsvMapper : ClassMap<MillenniumTransaction>
    {
        public MillenniumCsvMapper()
        {
            Map(x => x.TransactionDateUtc).Name("Data transakcji");
            Map(x => x.RelatedAccountNumber).Name("Na konto/Z konta");
            Map(x => x.Customer).Convert(args =>
            {
                var isCustomer = args.Row.TryGetField<string>("Odbiorca/Zleceniodawca", out var customer);
                return isCustomer ? Trim(customer) : "";
            });
            Map(x => x.Description).Convert(args =>
            {
                var isDescription = args.Row.TryGetField<string>("Opis", out var description);
                return isDescription ? Trim(description) : "";
            });
            Map(x => x.Value).Convert(args =>
            {
                var isCharge = args.Row.TryGetField<decimal>("Obciążenia", out var charge);
                var isIncome = args.Row.TryGetField<decimal>("Uznania", out var income);
                return isCharge ? charge : income;
            }).Default(0);
            Map(x => x.AccountBalance).Name("Saldo");
        }

        public string Trim(string value)
        {
            Regex trimmer = new Regex(@"\s\s+");
            return trimmer.Replace(value, " ");
        }
    }
}
