using CMS.Application.Feature.Authentications.Users.Dtos;
using CMS.Application.Feature.Authentications.Users.Request;
using CMS.Application.Feature.Authentications.Users.Services;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Users.Handler
{
    public class LoginUserHandler(IUserServices userServices) : IRequestHandler<LoginUser, IResult<UserAuthenticationDto>>
    {
        public async Task<IResult<UserAuthenticationDto>> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            return await userServices.Login(request, cancellationToken);
        }
    }
}
