using CMS.Application.Feature.Authentications.Users.Dtos;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Users.Request
{
    public class LoginUser : IRequest<IResult<UserAuthenticationDto>>
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
