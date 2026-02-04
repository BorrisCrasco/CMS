using CMS.Application.Feature.Events.Request;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.WebApi.Controllers.Events
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateEvent command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] GetEvent command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpGet()]
        public async Task<IActionResult> GetPaginated([FromQuery] GetEvents command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}
