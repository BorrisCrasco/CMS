using CMS.Application.Feature.Events.Dtos;
using CMS.Application.Feature.Members.Dtos;
using CMS.Application.Feature.Members.Request;
using CMS.Application.Feature.Members.Services;
using Lipip.Atomic.EntityFramework.Core.Paginations;
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
    public class GetMembersHandler(IMemberServices memberServices, IMapper mapper)
        : IRequestHandler<GetMembers, IResult<PagedResult<MemberResultDto>>>
    {
        public async Task<IResult<PagedResult<MemberResultDto>>> Handle(GetMembers request, CancellationToken cancellationToken)
        {
            var query = await memberServices.Gets(request, cancellationToken);

            return Result<PagedResult<MemberResultDto>>.Success(query);
            
        }
    }
}
