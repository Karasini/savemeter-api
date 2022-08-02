using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaveMeter.Services.Finances.Application.Queries;

namespace SaveMeter.Services.Finances.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MoneySourcesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MoneySourcesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetMoneySources([FromQuery] GetMoneySourcesQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
