using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaveMeter.Services.Finances.Application.Commands.CreateFinancialGoal;
using SaveMeter.Services.Finances.Application.Commands.CreateFinancialGoalGroup;
using SaveMeter.Services.Finances.Application.Commands.CreateMoneySource;
using SaveMeter.Services.Finances.Application.Commands.DeleteFinancialGoal;
using SaveMeter.Services.Finances.Application.Commands.UpdateFinancialGoal;
using SaveMeter.Services.Finances.Application.Commands.UpdateMoneySource;
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
        public async Task<IActionResult> GetFinancialGoalGroups([FromQuery] GetFinancialGoalGroupsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost("{goalGroupId:guid}/goals")]
        public async Task<IActionResult> CreateFinancialGoal([FromBody] CreateFinancialGoalCommand command, Guid goalGroupId)
        {
            command.GoalGroupId = goalGroupId;
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{goalGroupId:guid}/goals/{id:guid}")]
        public async Task<IActionResult> UpdateFinancialGoal([FromBody] UpdateFinancialGoalCommand command, Guid goalGroupId, Guid id)
        {
            command.Id = id;
            command.GoalGroupId = goalGroupId;
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{goalGroupId:guid}/goals/{id:guid}")]
        public async Task<IActionResult> DeleteFinancialGoal(Guid goalGroupId, Guid id)
        {
            var command = new DeleteFinancialGoalCommand
            {
                GoalGroupId = goalGroupId,
                Id = id
            };

            return Ok(await _mediator.Send(command));
        }
    }
}
