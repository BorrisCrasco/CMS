using CMS.Application.Feature.Authentications.Roles.Dtos;
using CMS.Application.Feature.Authentications.Roles.Request;
using CMS.Application.Feature.Authentications.Roles.Services;
using Lipip.Atomic.EntityFramework.Core.Paginations;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Roles.Handler
{
    public class GetRolesQueryHandler(IRoleServices roleServices) : IRequestHandler<GetRolesQuery, PagedResult<RoleResultDto>>
    {
        public async Task<PagedResult<RoleResultDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            return await roleServices.GetPaged(request, cancellationToken);
        }
    }
}
