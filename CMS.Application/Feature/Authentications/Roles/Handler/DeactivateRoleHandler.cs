using Cms.Persistence.Models;
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
    public class DeactivateRoleHandler(IRoleServices roleServices) : IRequestHandler<DeactivateRole, IResult<int>>
    {
        public async Task<IResult<int>> Handle(DeactivateRole request, CancellationToken cancellationToken)
        {
            return await roleServices.Deactivate(request.Id, cancellationToken);
        }
    }
}
