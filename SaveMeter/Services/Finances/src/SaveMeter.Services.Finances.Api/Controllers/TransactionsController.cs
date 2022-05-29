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
                await CreateTransactionsFromFile(formFile);
            }

            return Ok();

            var encodingType = CharsetDetector.DetectFromStream(Request.Body);
            Request.Body.Position = 0;

            using var streamReader = new StreamReader(Request.Body, encodingType.Detected.Encoding);
            using var csv = new CsvReader(streamReader, new CsvConfiguration(CultureInfo.InvariantCulture) { BadDataFound = null, DetectDelimiter = true });
            await csv.ReadAsync();

            csv.ReadHeader();

            if (csv.CanRead<IngCsvMapper>()) csv.Context.RegisterClassMap<IngCsvMapper>();
            if (csv.CanRead<MillenniumCsvMapper>()) csv.Context.RegisterClassMap<MillenniumCsvMapper>();

            var records = csv.GetRecordsAsync<CreateTransactionCommand>();

            await foreach (var command in records)
            {
                await _mediator.Send(command);
            }

            return Ok();
        }

        private async Task CreateTransactionsFromFile(IFormFile obj)
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
                    catch (Exception e)
                    {

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
                    catch (Exception e)
                    {

                    }
                    reading = false;
                }
            }

            foreach (var command in fooRecords)
            {
                await _mediator.Send(new CreateTransactionCommand
                {
                    AccountNumber = command.AccountNumber,
                    TransactionDate = command.TransactionDate,
                    RelatedAccountNumber = command.RelatedAccountNumber,
                    Customer = command.Customer,
                    Description = command.Description,
                    Value = command.Value,
                    AccountBalance = command.AccountBalance
                });
            }

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

