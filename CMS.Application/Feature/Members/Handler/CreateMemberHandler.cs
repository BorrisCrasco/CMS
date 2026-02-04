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
    public class CreateMemberHandler(IMemberServices memberServices) : IRequestHandler<CreateMember, IResult<Guid>>
    {
        public async Task<IResult<Guid>> Handle(CreateMember request, CancellationToken cancellationToken)
        {
            var create = await memberServices.Create(request.Member,cancellationToken);

            return Result<Guid>.Success(create.Id);
        }
    }
}
