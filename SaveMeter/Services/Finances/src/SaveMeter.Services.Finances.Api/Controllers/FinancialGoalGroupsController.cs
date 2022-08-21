using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaveMeter.Services.Finances.Application.Commands.CreateFinancialGoalGroup;
using SaveMeter.Services.Finances.Application.Commands.CreateMoneySource;
using SaveMeter.Services.Finances.Application.Queries;

namespace SaveMeter.Services.Finances.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FinancialGoalGroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FinancialGoalGroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFinancialGoalGroup([FromBody] CreateFinancialGoalGroupCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetMoneySources([FromQuery] GetFinancialGoalGroupsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

    }
}
