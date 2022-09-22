using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaveMeter.Modules.Users.Core.Commands;
using SaveMeter.Shared.Abstractions.Contexts;
using SaveMeter.Shared.Abstractions.Dispatchers;
using SaveMeter.Shared.Infrastructure.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace SaveMeter.Modules.Users.Api.Controllers;

[ApiController]
[Route("[controller]")]
[ProducesDefaultContentType]
internal class AccountController : ControllerBase
{
    private const string AccessTokenCookie = "__access-token";
    private readonly IDispatcher _dispatcher;
    private readonly IContext _context;
    private readonly CookieOptions _cookieOptions;

    public AccountController(IDispatcher dispatcher, IContext context, CookieOptions cookieOptions)
    {
        _dispatcher = dispatcher;
        _context = context;
        _cookieOptions = cookieOptions;
    }

    [HttpPost("sign-up")]
    [SwaggerOperation("Sign up")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignUpAsync(SignUp command)
    {
        var result = await _dispatcher.SendAsync<SignUp, string>(command);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("test")]
    [SwaggerOperation("Test")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignUpAsync()
    {
        return NoContent();
    }
}