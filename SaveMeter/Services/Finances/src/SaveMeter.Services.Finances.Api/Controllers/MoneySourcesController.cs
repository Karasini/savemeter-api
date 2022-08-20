using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaveMeter.Services.Finances.Application.Commands.CreateMoneySource;
using SaveMeter.Services.Finances.Application.Commands.UpdateTransaction;
using SaveMeter.Services.Finances.Application.Queries;

namespace SaveMeter.Services.Finances.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MoneySourcesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MoneySourcesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetMoneySources([FromQuery] GetMoneySourcesQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        public async Task<IActionResult> CreateMoneySource([FromBody] CreateMoneySourceCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

                [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTransaction([FromBody] UpdateTransactionCommand command, Guid id)
        {
            command.Id = id;
            return Ok(await _mediator.Send(command));
        }
    }
}
