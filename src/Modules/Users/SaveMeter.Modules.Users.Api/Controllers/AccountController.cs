using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaveMeter.Modules.Users.Core.Commands;
using SaveMeter.Modules.Users.Core.DTO;
using SaveMeter.Modules.Users.Core.Queries;
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

    [HttpGet]
    [Authorize]
    [SwaggerOperation("Get account of logged user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDetailsDto>> GetAccount()
    {
        var user = await _dispatcher.QueryAsync(new GetUser { UserId = _context.GetUserId() });
        if (user != null)
        {
            return Ok(user.Bind(x => x.ExpirationTime, _context.Identity.ExpirationTime.UtcDateTime));
        }
        return NotFound();
    }

    [HttpPost("sign-up")]
    [SwaggerOperation("Sign up")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignUpAsync(SignUp command)
    {
        await _dispatcher.SendAsync(command);
        return NoContent();
    }

    [HttpPost("sign-in")]
    [SwaggerOperation("Sign in")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDetailsDto>> SignInAsync(SignIn command)
    {
        var jwt = await _dispatcher.RequestAsync(command);
        AddCookie(AccessTokenCookie, jwt.AccessToken);
        
        return NoContent();
    }

    [Authorize]
    [HttpDelete("sign-out")]
    [SwaggerOperation("Sign out")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public Task<ActionResult> SignOutAsync()
    {
        DeleteCookie(AccessTokenCookie);
        return Task.FromResult<ActionResult>(NoContent());
    }

    private void AddCookie(string key, string value) => Response.Cookies.Append(key, value, _cookieOptions);

    private void DeleteCookie(string key) => Response.Cookies.Delete(key, _cookieOptions);
}