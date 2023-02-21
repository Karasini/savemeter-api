using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using SaveMeter.Modules.Transactions.Api.Csv.Mappers;
using UtfUnknown;

namespace SaveMeter.Modules.Transactions.Api.Csv;

internal class TransactionImporter : ITransactionImporter
{
    private readonly CsvOptions _options;
    
    private readonly List<ClassMap> _maps;
    public TransactionImporter(CsvOptions options)
    {
        _options = options;
        _maps = new List<ClassMap>
        {
            new IngCsvMapper(),
            new MillenniumCsvMapper()
        };
    }

    public async Task<List<CsvTransaction>> Import(Stream stream)
    {
        var encodingType = CharsetDetector.DetectFromStream(stream);
        stream.Seek(0, SeekOrigin.Begin);
        
        using var reader = new StreamReader(stream, encodingType.Detected.Encoding);
        using var csv = new CsvReader(reader,
            new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                BadDataFound = null,
                DetectDelimiter = true,
                Encoding = encodingType.Detected.Encoding,
                Delimiter = _options.Delimiter
            });

        _maps.ForEach(map => csv.Context.RegisterClassMap(map.GetType()));
        // csv.Context.RegisterClassMap<IngCsvMapper>();
        // csv.Context.RegisterClassMap<MillenniumCsvMapper>();
        var fooRecords = new List<CsvTransaction>();

        var reading = true;
        while (reading)
        {
            await csv.ReadAsync();
            csv.ReadHeader();
            var mapType = _maps.FirstOrDefault(x => csv.CanRead(x));

            if (mapType == null)
            {
                continue;
            }

            var recordType = mapType.GetType().BaseType.GetGenericArguments()[0];
            var records = csv.GetRecordsAsync(recordType);
            try
            {
                await foreach (var record in records)
                {
                    fooRecords.Add((CsvTransaction)record);
                }
            }
            catch (Exception)
            {
                // ignores error because some csv files have noise inside, so code reads what can, and stops in case of error.
            }
            
            reading = false;
        }

        return fooRecords;
    }
}