using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaveMeter.Shared.Abstractions.Dispatchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SaveMeter.Modules.Transactions.Api;
using SaveMeter.Modules.Transactions.Core.DTO;
using SaveMeter.Modules.Transactions.Core.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace SaveMeter.Modules.Categories.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
internal class CategoriesController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public CategoriesController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet]
    [Authorize(TransactionsPolicies.CategoriesRead)]
    [SwaggerOperation("Get categories list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<List<CategoryDto>>> BrowseAsync([FromQuery] GetCategories query)
        => Ok(await _dispatcher.QueryAsync(query));
}
