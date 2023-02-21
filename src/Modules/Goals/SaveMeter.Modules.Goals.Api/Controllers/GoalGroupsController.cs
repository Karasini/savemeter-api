using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaveMeter.Modules.Goals.Core.Commands;
using SaveMeter.Modules.Goals.Core.DTO;
using SaveMeter.Modules.Goals.Core.Queries;
using SaveMeter.Shared.Abstractions.Contexts;
using SaveMeter.Shared.Abstractions.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;

namespace SaveMeter.Modules.Goals.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
internal class GoalGroupsController : ControllerBase
{
    private readonly IDispatcher _dispatcher;
    private readonly IContext _context;

    public GoalGroupsController(IDispatcher dispatcher, IContext context)
    {
        _dispatcher = dispatcher;
        _context = context;
    }
    
    [HttpPost]
    [Authorize(Claims.GoalsCrud)]
    [SwaggerOperation("Create goal group")]
    [ProducesResponseType(typeof(GoalsGroupDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(GoalsGroupDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateFinancialGoalGroup([FromBody] CreateGoalGroup command)
    {
        return Ok(await _dispatcher.RequestAsync(command));
    }

    [HttpGet]
    [Authorize(Claims.GoalsRead)]
    [SwaggerOperation("Get goals list")]
    [ProducesResponseType(typeof(List<GoalsGroupDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetFinancialGoalGroups([FromQuery] GetGoalsGroups query)
    {
        return Ok(await _dispatcher.QueryAsync(query));
    }
    
    [HttpPost("{goalGroupId:guid}/goals")]
    [Authorize(Claims.GoalsCrud)]
    [SwaggerOperation("Create goal")]
    [ProducesResponseType(typeof(GoalsGroupDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(GoalsGroupDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateFinancialGoal([FromBody] CreateGoal command, Guid goalGroupId)
    {
        command.GoalGroupId = goalGroupId;
        return Ok(await _dispatcher.RequestAsync(command));
    }

    [HttpPut("{goalGroupId:guid}/goals/{id:guid}")]
    [Authorize(Claims.GoalsCrud)]
    [SwaggerOperation("Update goal")]
    [ProducesResponseType(typeof(GoalsGroupDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GoalsGroupDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateFinancialGoal([FromBody] UpdateGoal command, Guid goalGroupId, Guid id)
    {
        command.Id = id;
        command.GoalGroupId = goalGroupId;
        return Ok(await _dispatcher.RequestAsync(command));
    }

    [HttpDelete("{goalGroupId:guid}/goals/{id:guid}")]
    [Authorize(Claims.GoalsCrud)]
    [SwaggerOperation("Delete goal")]
    [ProducesResponseType(typeof(GoalsGroupDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(GoalsGroupDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFinancialGoal(Guid goalGroupId, Guid id)
    {
        var command = new DeleteGoal
        {
            GoalGroupId = goalGroupId,
            Id = id
        };

        await _dispatcher.SendAsync(command);
            
        return NoContent();
    }
}
