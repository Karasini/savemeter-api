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
    private readonly ITransactionImporter _importer;

    public TransactionsController(IDispatcher dispatcher, IContext context, ITransactionImporter importer)
    {
        _dispatcher = dispatcher;
        _context = context;
        _importer = importer;
    }

    [HttpPost("csv")]
    [Authorize(TransactionsPolicies.TransactionsCrud)]
    [SwaggerOperation("Import list of transactions in csv format")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PostCsv(List<IFormFile> file)
    {
        foreach (var formFile in file)
        {
            var transactions = await _importer.Import(formFile.OpenReadStream());
            var importTransactionCommand = new ImportTransactions
            {
                Transactions = transactions.Select(command => new ImportTransactions.Transaction
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

        return Ok();
    }

    [HttpPost("train")]
    [Authorize(TransactionsPolicies.TransactionsCrud)]
    [SwaggerOperation("Train ML model")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> TrainModel(TrainModel model)
    {
        await _dispatcher.SendAsync(model.Bind(x => x.UserId, _context.Identity.Id));
        return NoContent();
    }

    [HttpPost]
    [Authorize(TransactionsPolicies.TransactionsCrud)]
    [SwaggerOperation("Create bank transaction")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTransaction(CreateTransaction command)
    {
        var result = await _dispatcher.RequestAsync(command.Bind(x => x.UserId, _context.Identity.Id));
        return Created("", result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(TransactionsPolicies.TransactionsCrud)]
    [SwaggerOperation("Update transaction")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateTransaction([FromBody] UpdateTransaction command, Guid id)
    {
        return Ok(await _dispatcher.RequestAsync(command
            .Bind(x => x.Id, id)
            .Bind(x => x.UserId, _context.GetUserId())));
    }

    [HttpGet]
    [Authorize(TransactionsPolicies.TransactionsRead)]
    [SwaggerOperation("Get transactions list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBankTransactionsByFilter([FromQuery] GetBankTransactionsByFilter query)
    {
        return Ok(await _dispatcher.QueryAsync(query.Bind(x => x.UserId, _context.Identity.Id)));
    }
    
    [HttpGet("groupedTransactions")]
    [Authorize(TransactionsPolicies.TransactionsRead)]
    [SwaggerOperation("Get grouped transactions list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBankTransactionsByFilter([FromQuery] GetGroupedBankTransactions query)
    {
        return Ok(await _dispatcher.QueryAsync(query.Bind(x => x.UserId, _context.Identity.Id)));
    }
}