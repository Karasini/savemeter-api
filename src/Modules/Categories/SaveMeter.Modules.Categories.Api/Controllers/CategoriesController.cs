using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaveMeter.Shared.Abstractions.Dispatchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMeter.Modules.Categories.Api.Controllers;

[Authorize(Policy)]
internal class CategoriesController : ControllerBase
{
    private const string Policy = "categories";
    
    private readonly IDispatcher _dispatcher;

    public CategoriesController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
}
