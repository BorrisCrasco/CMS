using CMS.Application.Feature.Events.Request;
using CMS.Application.Feature.Members.Dtos;
using CMS.Application.Feature.Members.Request;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Cms.WebApi.Controllers.Maintenance.Member
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType<MemberDto>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] CreateMember command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpGet("{id}")]
        [ProducesResponseType<MemberDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id )
        {
            var query = new GetMember
            {
                Id = id
            };

            return Ok(await mediator.Send(query));
        }
    }
}
