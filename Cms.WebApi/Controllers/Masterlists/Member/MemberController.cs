using CMS.Application.Feature.Masterlists.Members.Dtos;
using CMS.Application.Feature.Masterlists.Members.Request;
using Lipip.Atomic.EntityFramework.Core.Paginations;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Cms.WebApi.Controllers.Masterlists.Member
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController(IMediator mediator) : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<MemberResultDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Search([FromQuery] GetMembersQuery query)
        {
            return Ok(await mediator.Send(query));
        }

        [HttpPost]
        [ProducesResponseType<IResult<MemberDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] CreateMember command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpGet("{id}")]
        [ProducesResponseType<IResult<MemberDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id )
        {
            var query = new GetMember
            {
                Id = id
            };

            return Ok(await mediator.Send(query));
        }

        [HttpPut("{id}")]
        [ProducesResponseType<IResult<MemberDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromRoute]Guid id,[FromBody] UpdateMember command)
        {
            command.Member.Id = id;
            return Ok(await mediator.Send(command));
        }

        [HttpPatch("{id}/deactivate")]
        [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Deactivate([FromRoute]Guid id)
        {
            var command = new DeactivateMember
            {
                Id = id
            };
            return Ok(await mediator.Send(command));
        }

        [HttpPatch("{id}/reactivate")]
        [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Reactivate([FromRoute] Guid id)
        {
            var command = new ReactivateMember
            {
                Id = id
            };
            return Ok(await mediator.Send(command));
        }


        [HttpGet("genders")]
        [ProducesResponseType<IResult<IEnumerable<GenderDto>>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGenders([FromQuery] GetGenders query )
        {

            return Ok(await mediator.Send(query));
        }


    }
}
