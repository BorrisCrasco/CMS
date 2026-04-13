
using CMS.Application.Feature.Authentications.Menus.Dtos;
using CMS.Application.Feature.Authentications.Menus.Request;
using CMS.Application.Feature.Authentications.Menus.Services;
using CMS.Application.Feature.Authentications.Roles.Dtos;
using CMS.Application.Feature.Authentications.Roles.Request;
using CMS.Application.Feature.Authentications.Roles.Services;
using Lipip.Atomic.EntityFramework.Common.Paginations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Menus.Handler
{
    public class GetMenuQueryHandler(IMenuServices menuServices) : IRequestHandler<GetMenuQuery, PagedResult<MenuResultDto>>
    {
        public async Task<PagedResult<MenuResultDto>> Handle(GetMenuQuery request, CancellationToken cancellationToken)
        {
            return await menuServices.GetPaged(request, cancellationToken);
        }
    }
}
