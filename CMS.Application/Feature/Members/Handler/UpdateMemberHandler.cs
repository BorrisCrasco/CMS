using Cms.Persistence.Models;
using CMS.Application.Feature.Members.Request;
using CMS.Application.Feature.Members.Services;
using Lipip.Atomic.EntityFramework.Result;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Members.Handler
{
    public class UpdateMemberHandler(IMemberServices memberServices) 
        : IRequestHandler<UpdateMember, IResult<Member>>
    {
        public async Task<IResult<Member>> Handle(UpdateMember request, CancellationToken cancellationToken)
        {

           return await memberServices.Update(request.Member,cancellationToken);

        }
    }
}
