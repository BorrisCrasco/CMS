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
    public class ReactivateMemberHandler(IMemberServices memberServices)
        : IRequestHandler<ReactivateMember, IResult<Guid>>
    {
        public async Task<IResult<Guid>> Handle(ReactivateMember request, CancellationToken cancellationToken)
        {
            return await memberServices.ReactivateMember(request.Id, cancellationToken);
        }
    }
}
