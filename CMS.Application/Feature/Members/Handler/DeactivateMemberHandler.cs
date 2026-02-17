using CMS.Application.Feature.Members.Request;
using CMS.Application.Feature.Members.Services;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Members.Handler
{
    public class DeactivateMemberHandler(IMemberServices memberServices) 
        : IRequestHandler<DeactivateMember, IResult<Guid>>
    {
        public async Task<IResult<Guid>> Handle(DeactivateMember request, CancellationToken cancellationToken)
        {
            return await memberServices.DeactivateMember(request.Id, cancellationToken);
        }
    }
}
