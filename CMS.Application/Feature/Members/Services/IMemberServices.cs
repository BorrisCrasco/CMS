using Cms.Persistence.Models;
using CMS.Application.Feature.Members.Dtos;
using CMS.Application.Feature.Members.Request;
using Lipip.Atomic.EntityFramework.Core.Paginations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Members.Services
{
    public interface IMemberServices
    {
        Task<Member> Create(MemberDto member, CancellationToken cancellationToken = default);

        Task<MemberDto> Get(Guid Id, CancellationToken cancellationToken = default);

        Task<PagedResult<MemberResultDto>> Gets(GetMembers request,CancellationToken cancellationToken = default);

    }
}
