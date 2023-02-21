using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SaveMeter.Modules.Transactions.Api.Csv;

internal interface ITransactionImporter
{
    Task<List<CsvTransaction>> Import(Stream stream);
}