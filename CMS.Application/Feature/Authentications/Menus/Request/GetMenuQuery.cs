using CMS.Application.Feature.Authentications.Menus.Dtos;
using CMS.Application.Feature.Authentications.Roles.Dtos;
using Lipip.Atomic.EntityFramework.Common.Paginations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Menus.Request
{
    public class GetMenuQuery : IRequest<PagedResult<MenuResultDto>>
    {
        public string? Search { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
