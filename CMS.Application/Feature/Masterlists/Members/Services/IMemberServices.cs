using Cms.Persistence.Models;
using CMS.Application.Feature.Masterlists.Members.Dtos;
using CMS.Application.Feature.Masterlists.Members.Request;
using Lipip.Atomic.EntityFramework.Core.Paginations;
using Lipip.Atomic.EntityFramework.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Masterlists.Members.Services
{
    public interface IMemberServices
    {
        Task<IResult<Member>> Create(MemberDto member, CancellationToken cancellationToken = default);

        Task<IResult<MemberDto>> Get(Guid Id, CancellationToken cancellationToken = default);

        Task<PagedResult<MemberResultDto>> GetPaged(GetMembersQuery request,CancellationToken cancellationToken = default);

        Task<IResult<Member>> Update(MemberDto member, CancellationToken cancellationToken = default);

        Task<IResult<Guid>> ReactivateMember (Guid id , CancellationToken cancellationToken = default);

        Task<IResult<Guid>> DeactivateMember(Guid id, CancellationToken cancellationToken = default);

        Task<IResult<IEnumerable<GenderDto>>> GetGenders(CancellationToken cancellationToken = default);

        Task<bool> GenderExist(int id, CancellationToken cancellationToken); 

    }
}
