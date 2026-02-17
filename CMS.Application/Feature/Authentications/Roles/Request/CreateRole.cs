using Cms.Persistence.Models;
using CMS.Application.Feature.Authentications.Roles.Dtos;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Roles.Request
{
    public class CreateRole : IRequest<IResult<RoleDto>>
    {
        public RoleDto Role {  get; set; }
    }
}
