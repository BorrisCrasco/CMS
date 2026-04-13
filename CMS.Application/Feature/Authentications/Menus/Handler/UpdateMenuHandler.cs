using CMS.Application.Feature.Authentications.Menus.Dtos;
using CMS.Application.Feature.Authentications.Menus.Request;
using CMS.Application.Feature.Authentications.Menus.Services;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Menus.Handler
{
    public class UpdateMenuHandler(IMenuServices menuServices) 
        : IRequestHandler<UpdateMenu, IResult<MenuDto>>
    {
        public async Task<IResult<MenuDto>> Handle(UpdateMenu request, CancellationToken cancellationToken)
        {
            return await menuServices.Update(request.Menu, cancellationToken);
        }
    }
}
