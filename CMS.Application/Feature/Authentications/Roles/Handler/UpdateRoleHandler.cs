using Cms.Persistence.Models;
using CMS.Application.Feature.Authentications.Roles.Dtos;
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
    public class UpdateRoleHandlerr(IRoleServices roleServices) : IRequestHandler<UpdateRole, IResult<RoleDto>>
    {
        public async Task<IResult<RoleDto>> Handle(UpdateRole request, CancellationToken cancellationToken)
        {
            return await roleServices.Update(request.Role, cancellationToken);
        }
    }
}
