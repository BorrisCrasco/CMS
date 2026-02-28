using CMS.Application.Feature.Masterlists.Members.Dtos;
using CMS.Application.Feature.Masterlists.Members.Request;
using CMS.Application.Feature.Masterlists.Members.Services;
using Lipip.Atomic.EntityFramework.Common.Paginations;
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
    public class GetMembersQueryHandler(IMemberServices memberServices)
        : IRequestHandler<GetMembersQuery, PagedResult<MemberResultDto>>
    {
        public async Task<PagedResult<MemberResultDto>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
        {
            return await memberServices.GetPaged(request, cancellationToken);
            
        }
    }
}
