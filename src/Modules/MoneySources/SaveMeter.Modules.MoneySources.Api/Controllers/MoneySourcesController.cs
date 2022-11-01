using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaveMeter.Modules.MoneySources.Core.Commands;
using SaveMeter.Modules.MoneySources.Core.DTO;
using SaveMeter.Modules.MoneySources.Core.Queries;
using SaveMeter.Shared.Abstractions.Dispatchers;

namespace SaveMeter.Modules.MoneySources.Api.Controllers;

[Authorize(Policy)]
internal class MoneySourcesController : ControllerBase
{
    private const string Policy = "moneySources";
    
    private readonly IDispatcher _dispatcher;

    public MoneySourcesController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetMoneySources([FromQuery] GetMoneySources query)
    {
        return Ok(await _dispatcher.QueryAsync(query));
    }

    [HttpPost]
    public async Task<IActionResult> CreateMoneySource([FromBody] CreateMoneySource command)
    {
        return Ok(await _dispatcher.SendAsync<MoneySourceDto>(command));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTransaction([FromBody] UpdateMoneySource command, Guid id)
    {
        command.Id = id;
        return Ok(await _dispatcher.SendAsync<MoneySourceDto>(command));
    }
}
