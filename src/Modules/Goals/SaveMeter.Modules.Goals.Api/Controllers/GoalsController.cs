using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaveMeter.Shared.Abstractions.Dispatchers;

namespace SaveMeter.Modules.Goals.Api.Controllers;

[Authorize(Policy)]
internal class GoalsController : ControllerBase
{
    private const string Policy = "goals";
    
    private readonly IDispatcher _dispatcher;

    public GoalsController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
}
