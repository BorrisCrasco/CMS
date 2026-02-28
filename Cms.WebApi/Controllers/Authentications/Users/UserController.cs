using CMS.Application.Feature.Authentications.Roles.Dtos;
using CMS.Application.Feature.Authentications.Roles.Request;
using CMS.Application.Feature.Authentications.Users.Dtos;
using CMS.Application.Feature.Authentications.Users.Request;
using Lipip.Atomic.EntityFramework.Common.Paginations;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.WebApi.Controllers.Authentications.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType<IResult<UserAuthenticationDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginUser command)
        {
            return Ok(await mediator.Send(command));
        }



        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<UserResultDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Search([FromQuery] GetUsersQuery query)
        {
            return Ok(await mediator.Send(query));
        }

        [HttpPost]
        [ProducesResponseType<IResult<UserDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] CreateUser command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpGet("{id}")]
        [ProducesResponseType<IResult<UserDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var query = new GetUser
            {
                Id = id
            };

            return Ok(await mediator.Send(query));
        }

        [HttpPut("{id}")]
        [ProducesResponseType<IResult<UserDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUser command)
        {
            command.User.Id = id;
            return Ok(await mediator.Send(command));
        }

        [HttpPatch("{id}/deactivate")]
        [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Deactivate([FromRoute] Guid id)
        {
            var command = new DeactivateUser
            {
                Id = id
            };
            return Ok(await mediator.Send(command));
        }

        [HttpPatch("{id}/reactivate")]
        [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Reactivate([FromRoute] Guid id)
        {
            var command = new DeactivateUser
            {
                Id = id
            };
            return Ok(await mediator.Send(command));
        }
    }
}
