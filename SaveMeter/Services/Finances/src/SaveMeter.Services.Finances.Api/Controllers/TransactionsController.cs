﻿using MediatR;
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
        public async Task<IActionResult> PostCsv()
        {
            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { BadDataFound = null, DetectDelimiter = true});
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

