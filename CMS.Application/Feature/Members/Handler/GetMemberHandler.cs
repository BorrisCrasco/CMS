using CMS.Application.Feature.Members.Dtos;
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
    public class GetMemberHandler(IMemberServices memberServices , IMapper mapper) : IRequestHandler<GetMember, IResult<MemberDto>>
    {
        public async Task<IResult<MemberDto>> Handle(GetMember request, CancellationToken cancellationToken)
        {
            var model = await memberServices.Get(request.Id, cancellationToken);

            return Result.Success(model);
        }
    }
}
