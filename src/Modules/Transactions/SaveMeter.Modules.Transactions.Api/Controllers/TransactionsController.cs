using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaveMeter.Modules.Transactions.Api.Csv;
using SaveMeter.Shared.Abstractions.Dispatchers;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Linq;
using UtfUnknown;
using SaveMeter.Modules.Transactions.Core.Commands;
using SaveMeter.Modules.Transactions.Core.DTO;
using SaveMeter.Modules.Transactions.Core.Queries;
using SaveMeter.Shared.Abstractions.Contexts;
using SaveMeter.Shared.Infrastructure.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace SaveMeter.Modules.Transactions.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
internal class TransactionsController : ControllerBase
{
    private readonly IDispatcher _dispatcher;
    private readonly IContext _context;

    public TransactionsController(IDispatcher dispatcher, IContext context)
    {
        _dispatcher = dispatcher;
        _context = context;
    }

    [HttpPost("csv")]
    [Authorize(TransactionsPolicies.TransactionsCrud)]
    [SwaggerOperation("Import list of transactions in csv format")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PostCsv(List<IFormFile> file)
    {
        foreach (var formFile in file)
        {
            await TransactionsFromFile(formFile);
        }

        return Ok();
    }

    [HttpPost]
    [Authorize(TransactionsPolicies.TransactionsCrud)]
    [SwaggerOperation("Train ML model")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> TrainModel(TrainModel model)
    {
        await _dispatcher.SendAsync(model);
        return NoContent();
    }
    
    [HttpPost]
    [Authorize(TransactionsPolicies.TransactionsCrud)]
    [SwaggerOperation("Create bank transaction")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTransaction(CreateTransaction command)
    {
        var result = await _dispatcher.SendAsync<BankTransactionDto>(command.Bind(x => x.UserId, _context.Identity.Id));
        return Created("", result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(TransactionsPolicies.TransactionsCrud)]
    [SwaggerOperation("Update transaction")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateTransaction([FromBody] UpdateTransaction command, Guid id)
    {
        return Ok(await _dispatcher.SendAsync<BankTransactionDto>(command
            .Bind(x => x.Id, id)
            .Bind(x => x.UserId, _context.Identity.Id)));
    }

    [HttpGet]
    [Authorize(TransactionsPolicies.TransactionsRead)]
    [SwaggerOperation("Get transactions list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBankTransactionsByFilter([FromQuery] GetBankTransactionsByFilter query)
    {
        return Ok(await _dispatcher.QueryAsync(query.Bind(x => x.UserId, _context.Identity.Id)));
    }

    private async Task TransactionsFromFile(IFormFile obj)
    {
        var encodingType = CharsetDetector.DetectFromStream(obj.OpenReadStream());
        using var reader = new StreamReader(obj.OpenReadStream(), encodingType.Detected.Encoding);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { BadDataFound = null, DetectDelimiter = true });
        csv.Context.RegisterClassMap<IngCsvMapper>();
        csv.Context.RegisterClassMap<MillenniumCsvMapper>();
        var fooRecords = new List<CsvTransaction>();

        var reading = true;
        while (reading)
        {
            await csv.ReadAsync();
            csv.ReadHeader();

            if (csv.CanRead<IngCsvMapper>())
            {
                var r = csv.GetRecordsAsync<IngTransaction>();
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
                var r = csv.GetRecordsAsync<MillenniumTransaction>();
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

        var importTransactionCommand = new ImportTransactions
        {
            Transactions = fooRecords.Select(command => new ImportTransactions.Transaction
            {
                TransactionDateUtc = DateTime.SpecifyKind(command.TransactionDateUtc, DateTimeKind.Utc),
                RelatedAccountNumber = command.RelatedAccountNumber,
                Customer = command.Customer,
                Description = command.Description,
                Value = command.Value,
                AccountBalance = command.AccountBalance,
                BankName = command.BankName,
                UserId = _context.Identity.Id
            }).ToList()
        };

        await _dispatcher.SendAsync(importTransactionCommand);

    }
}
