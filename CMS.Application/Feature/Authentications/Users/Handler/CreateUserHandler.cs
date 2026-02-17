using Cms.Persistence.Models;
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
    public class CreateUserHandler(IUserServices userServices) : IRequestHandler<CreateUser, IResult<UserDto>>
    {
        public async Task<IResult<UserDto>> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            return await userServices.Create(request.User, cancellationToken);
        }
    }
}
