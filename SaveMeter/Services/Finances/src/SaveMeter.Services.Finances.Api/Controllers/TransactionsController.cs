using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using SaveMeter.Services.Finances.Api.Csv;
using SaveMeter.Services.Finances.Application.Commands.CreateTransaction;
using SaveMeter.Services.Finances.Application.Commands.ImportTransactions;
using SaveMeter.Services.Finances.Application.Commands.UpdateTransaction;
using SaveMeter.Services.Finances.Application.Queries;
using UtfUnknown;

namespace SaveMeter.Services.Finances.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("csv")]
        public async Task<IActionResult> PostCsv(List<IFormFile> file)
        {
            foreach (var formFile in file)
            {
                await TransactionsFromFile(formFile);
            }

            return Ok();
        }

        private async Task TransactionsFromFile(IFormFile obj)
        {
            var encodingType = CharsetDetector.DetectFromStream(obj.OpenReadStream());
            using var reader = new StreamReader(obj.OpenReadStream(), encodingType.Detected.Encoding);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { BadDataFound = null, DetectDelimiter = true });
            csv.Context.RegisterClassMap<IngCsvMapper>();
            csv.Context.RegisterClassMap<MillenniumCsvMapper>();
            var fooRecords = new List<CreateTransactionCommand>();

            var reading = true;
            while (reading)
            {
                await csv.ReadAsync();
                csv.ReadHeader();
                if (csv.CanRead<IngCsvMapper>())
                {
                    var r = csv.GetRecordsAsync<IngTransactionCommand>();
                    try
                    {
                        await foreach (var record in r)
                        {
                            fooRecords.Add(record);
                        }
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    reading = false;
                }

                if (csv.CanRead<MillenniumCsvMapper>())
                {
                    var r = csv.GetRecordsAsync<MillenniumTransactionCommand>();
                    try
                    {
                        await foreach (var record in r)
                        {
                            fooRecords.Add(record);
                        }
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    reading = false;
                }
            }

            var importTransactionCommand = new ImportTransactionCommand
            {
                Transactions = fooRecords.Select(command => new ImportTransactionCommand.Transaction
                {
                    AccountNumber = command.AccountNumber,
                    TransactionDateUtc = DateTime.SpecifyKind(command.TransactionDateUtc, DateTimeKind.Utc),
                    RelatedAccountNumber = command.RelatedAccountNumber,
                    Customer = command.Customer,
                    Description = command.Description,
                    Value = command.Value,
                    AccountBalance = command.AccountBalance,
                    BankName = GetBankName(command)
                }).ToList()
            };

            await _mediator.Send(importTransactionCommand);

        }

        private string GetBankName(CreateTransactionCommand command)
        {
            return command switch
            {
                MillenniumTransactionCommand => "Millennium",
                IngTransactionCommand => "Ing",
                _ => ""
            };
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTransaction([FromBody] UpdateTransactionCommand command, Guid id)
        {
            command.Id = id;
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetBankTransactionsByFilter([FromQuery] GetBankTransactionsByFilterQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}

