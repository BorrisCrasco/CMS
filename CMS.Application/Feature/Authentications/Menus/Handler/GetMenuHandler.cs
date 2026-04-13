using CMS.Application.Feature.Authentications.Menus.Dtos;
using CMS.Application.Feature.Authentications.Menus.Request;
using CMS.Application.Feature.Authentications.Menus.Services;
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

namespace CMS.Application.Feature.Authentications.Menus.Handler
{
    public class GetMenuHandler(IMenuServices menuServices) : IRequestHandler<GetMenu, IResult<MenuDto>>
    {
        public async Task<IResult<MenuDto>> Handle(GetMenu request, CancellationToken cancellationToken)
        {
            return await menuServices.Get(request.Id, cancellationToken);
        }
    }
}
