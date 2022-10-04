using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
}
