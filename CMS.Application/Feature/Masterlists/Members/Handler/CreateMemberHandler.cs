using Cms.Persistence.Models;
using CMS.Application.Feature.Masterlists.Members.Request;
using CMS.Application.Feature.Masterlists.Members.Services;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Masterlists.Members.Handler
{
    public class CreateMemberHandler(IMemberServices memberServices) : IRequestHandler<CreateMember, IResult<Member>>
    {
        public async Task<IResult<Member>> Handle(CreateMember request, CancellationToken cancellationToken)
        {
            return await memberServices.Create(request.Member,cancellationToken);

        }
    }
}
