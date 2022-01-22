using Instapp.Services.Finances.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Instapp.Services.Finances.Application.Commands.CreateTransaction;
using SaveMeter.Services.Finances.Api.Csv;

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

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            using var reader = new StreamReader(Request.Body);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { BadDataFound = null });
            csv.Context.RegisterClassMap<MillenniumCsvMapper>();
            var records = csv.GetRecords<CreateTransactionCommand>().ToList();
            foreach (var command in records)
            {
                await _mediator.Send(command);
            }
            return Ok();
        }

    }
}

