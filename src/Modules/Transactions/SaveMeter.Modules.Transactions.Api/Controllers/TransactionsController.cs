using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaveMeter.Shared.Abstractions.Dispatchers;

namespace SaveMeter.Modules.Transactions.Api.Controllers;

[Authorize(Policy)]
internal class TransactionsController : ControllerBase
{
    private const string Policy = "transactions";
    
    private readonly IDispatcher _dispatcher;

    public TransactionsController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
}
