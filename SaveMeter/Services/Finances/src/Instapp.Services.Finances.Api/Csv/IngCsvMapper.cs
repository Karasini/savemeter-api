using CsvHelper.Configuration;
using Instapp.Services.Finances.Application.Commands.CreateTransaction;

namespace Instapp.Services.Finances.Api.Csv
{
    public sealed class IngCsvMapper : ClassMap<CreateTransactionCommand>
    {
        public IngCsvMapper()
        {
            //Map(x => x.AccountNumber).Name("Numer rachunku/karty");
            Map(x => x.TransactionDate).Name("Data transakcji");
            Map(x => x.SettlementDate).Name("Data księgowania");
            Map(x => x.RelatedAccountNumber).Name("Nr rachunku");
            Map(x => x.Customer).Name("Dane kontrahenta");
            Map(x => x.Description).Name("Tytuł");
            Map(x => x.Value).Name("Kwota transakcji (waluta rachunku)").Default(0);
            Map(x => x.AccountBalance).Name("Saldo po transakcji");
        }
    }
}
