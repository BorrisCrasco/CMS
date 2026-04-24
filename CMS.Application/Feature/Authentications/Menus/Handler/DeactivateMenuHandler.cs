using CMS.Application.Feature.Authentications.Menus.Request;
using CMS.Application.Feature.Authentications.Menus.Services;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;

namespace CMS.Application.Feature.Authentications.Menus.Handler
{
    public class DeactivateMenuHandler(IMenuServices menuServices) : IRequestHandler<DeactivateMenu, IResult<Guid>>
    {
        public async Task<IResult<Guid>> Handle(DeactivateMenu request, CancellationToken cancellationToken)
        {
            return await menuServices.Deactivate(request.Id, cancellationToken);
        }
    }
}
