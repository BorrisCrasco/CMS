using CMS.Application.Feature.Authentications.Roles.Request;
using CMS.Application.Feature.Authentications.Roles.Services;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Roles.Handler
{
    public class ReactivateRoleHandler(IRoleServices roleServices) : IRequestHandler<ReactivateRole, IResult<int>>
    {
        public async Task<IResult<int>> Handle(ReactivateRole request, CancellationToken cancellationToken)
        {
            return await roleServices.Reactivate(request.Id, cancellationToken);
        }
    }
}
