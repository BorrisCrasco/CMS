using Cms.Persistence.Models;
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
    public class ReactivateUserHandler(IUserServices userServices) : IRequestHandler<ReactivateUser, IResult<Guid>>
    {
        public async Task<IResult<Guid>> Handle(ReactivateUser request, CancellationToken cancellationToken)
        {
            return await userServices.Reactivate(request.Id, cancellationToken);
        }
    }
}
