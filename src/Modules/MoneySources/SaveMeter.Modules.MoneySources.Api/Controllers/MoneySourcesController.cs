using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaveMeter.Modules.MoneySources.Core.Commands;
using SaveMeter.Modules.MoneySources.Core.DTO;
using SaveMeter.Modules.MoneySources.Core.Queries;
using SaveMeter.Shared.Abstractions.Contexts;
using SaveMeter.Shared.Abstractions.Dispatchers;
using SaveMeter.Shared.Infrastructure.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace SaveMeter.Modules.MoneySources.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
internal class MoneySourcesController : ControllerBase
{
    private readonly IDispatcher _dispatcher;
    private readonly IContext _context;

    public MoneySourcesController(IDispatcher dispatcher, IContext context)
    {
        _dispatcher = dispatcher;
        _context = context;
    }
    
    [HttpGet]
    [Authorize(Claims.MoneySourcesRead)]
    [SwaggerOperation("Get money sources list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetMoneySources([FromQuery] GetMoneySources query)
    {
        return Ok(await _dispatcher.QueryAsync(query));
    }

    [HttpPost]
    [Authorize(Claims.MoneySourcesCrud)]
    [SwaggerOperation("Create money source")]
    [ProducesResponseType(typeof(MoneySourceDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMoneySource([FromBody] CreateMoneySource command)
    {
        return Ok(await _dispatcher.SendAsync<MoneySourceDto>(command
            .Bind(x => x.UserId, _context.GetUserId())));
    }

    [HttpPut("{id:guid}")]
    [Authorize(Claims.MoneySourcesCrud)]
    [SwaggerOperation("Update money source")]
    [ProducesResponseType(typeof(MoneySourceDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateTransaction([FromBody] UpdateMoneySource command, Guid id)
    {
        return Ok(await _dispatcher.SendAsync<MoneySourceDto>(command
            .Bind(x => x.Id, id)
            .Bind(x => x.UserId, _context.GetUserId())));
    }
}
