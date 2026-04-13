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
    public class ReactivateMenuHandler(IMenuServices menuServices) : IRequestHandler<ReactivateMenu, IResult<Guid>>
    {
        public async Task<IResult<Guid>> Handle(ReactivateMenu request, CancellationToken cancellationToken)
        {
            return await menuServices.Reactivate(request.Id, cancellationToken);
        }
    }
}
