using CMS.Application.Feature.Authentications.Roles.Dtos;
using CMS.Application.Feature.Masterlists.Members.Dtos;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Roles.Request
{
    public class GetRole : IRequest<IResult<RoleDto>>
    {

        public int Id { get; set; }
    }
}
