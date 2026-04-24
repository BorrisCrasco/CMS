using CMS.Application.Feature.Authentications.Menus.Dtos;
using CMS.Application.Feature.Authentications.Menus.Request;
using CMS.Application.Feature.Authentications.Menus.Services;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;

namespace CMS.Application.Feature.Authentications.Menus.Handler
{
    public class CreateMenuHandler(IMenuServices menuServices) : IRequestHandler<CreateMenu, IResult<MenuDto>>
    {
        public async Task<IResult<MenuDto>> Handle(CreateMenu request, CancellationToken cancellationToken)
        {
            return await menuServices.Create(request.Menu, cancellationToken);
        }
    }

}
