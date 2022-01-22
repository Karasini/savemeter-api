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
using SaveMeter.Services.Finances.Application.Commands.TrainNetwork;

namespace SaveMeter.Services.Finances.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MachineLearningController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MachineLearningController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("train")]
        public async Task<IActionResult> Post([FromBody] TrainNetworkCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

    }
}

