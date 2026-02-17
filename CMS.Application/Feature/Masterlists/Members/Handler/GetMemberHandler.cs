using CMS.Application.Feature.Masterlists.Members.Dtos;
using CMS.Application.Feature.Masterlists.Members.Request;
using CMS.Application.Feature.Masterlists.Members.Services;
using Lipip.Atomic.EntityFramework.Result;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Masterlists.Members.Handler
{
    public class GetMemberHandler(IMemberServices memberServices) : IRequestHandler<GetMember, IResult<MemberDto>>
    {
        public async Task<IResult<MemberDto>> Handle(GetMember request, CancellationToken cancellationToken)
        {
            return await memberServices.Get(request.Id, cancellationToken);

        }
    }
}
