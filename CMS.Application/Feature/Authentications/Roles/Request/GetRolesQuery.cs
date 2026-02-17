using CMS.Application.Feature.Authentications.Roles.Dtos;
using Lipip.Atomic.EntityFramework.Core.Paginations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Roles.Request
{
    public class GetRolesQuery : IRequest<PagedResult<RoleResultDto>>
    {
        public string? Search { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
