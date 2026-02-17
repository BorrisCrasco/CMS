using CMS.Application.Feature.Authentications.Roles.Dtos;
using CMS.Application.Feature.Authentications.Roles.Request;
using CMS.Application.Feature.Authentications.Users.Dtos;
using Lipip.Atomic.EntityFramework.Core.Paginations;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cms.WebApi.Controllers.Authentications.Roles
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<RoleResultDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Search([FromQuery] GetRolesQuery query)
        {
            return Ok(await mediator.Send(query));
        }

        [HttpPost]
        [ProducesResponseType<IResult<UserDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] CreateRole command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpGet("{id}")]
        [ProducesResponseType<IResult<RoleDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var query = new GetRole
            {
                Id = id
            };

            return Ok(await mediator.Send(query));
        }

        [HttpPut("{id}")]
        [ProducesResponseType<IResult<RoleDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRole command)
        {
            command.Role.Id = id;
            return Ok(await mediator.Send(command));
        }

        [HttpPatch("{id}/deactivate")]
        [ProducesResponseType<int>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {
            var command = new DeactivateRole
            {
                Id = id
            };
            return Ok(await mediator.Send(command));
        }

        [HttpPatch("{id}/reactivate")]
        [ProducesResponseType<int>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Reactivate([FromRoute] int id)
        {
            var command = new ReactivateRole
            {
                Id = id
            };
            return Ok(await mediator.Send(command));
        }
    }
}
