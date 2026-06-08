using CMS.Application.Feature.Authentications.Modules.Dtos;
using Lipip.Atomic.EntityFramework.Common.Paginations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Modules.Request
{
    public class GetModulesQuery : IRequest<PagedResult<ModuleResultDto>>
    {
        public string? Search { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
