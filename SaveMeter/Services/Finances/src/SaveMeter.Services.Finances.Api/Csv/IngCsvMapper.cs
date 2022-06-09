using CsvHelper.Configuration;
using SaveMeter.Services.Finances.Application.Commands.CreateTransaction;

namespace SaveMeter.Services.Finances.Api.Csv
{
    public class IngTransactionCommand : CreateTransactionCommand
    {
    }


    public sealed class IngCsvMapper : ClassMap<IngTransactionCommand>
    {
        public IngCsvMapper()
        {
            Map(x => x.TransactionDateUtc).Name("Data transakcji");
            Map(x => x.RelatedAccountNumber).Name("Nr rachunku");
            Map(x => x.Customer).Name("Dane kontrahenta");
            Map(x => x.Description).Name("Tytuł");
            //Map(x => x.Value).Name("Kwota transakcji (waluta rachunku)").Default(0);
            Map(x => x.Value).Convert(args =>
            {
                var isCharge = args.Row.TryGetField<decimal>("Kwota transakcji (waluta rachunku)", out var charge);
                return isCharge ? charge / 100 : 0;
            });
            Map(x => x.AccountBalance).Name("Saldo po transakcji");
        }
    }
}
