using Cms.Persistence.Models;
using CMS.Application.Feature.Authentications.Users.Dtos;
using CMS.Application.Feature.Authentications.Users.Request;
using CMS.Application.Feature.Authentications.Users.Services;
using Lipip.Atomic.EntityFramework.Common.Paginations;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Users.Handler
{
    public class GetUsersQueryHandler(IUserServices userServices) : IRequestHandler<GetUsersQuery, PagedResult<UserResultDto>>
    {
        public async Task<PagedResult<UserResultDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await userServices.GetPaged(request, cancellationToken);
        }
    }
}
  