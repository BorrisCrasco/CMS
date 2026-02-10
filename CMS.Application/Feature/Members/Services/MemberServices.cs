using Cms.Persistence.Models;
using CMS.Application.Feature.Members.Dtos;
using CMS.Application.Feature.Members.Request;
using Lipip.Atomic.EntityFramework.Core.Paginations;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Members.Services
{
    public class MemberServices(IMapper mapper, IMemberStore memberStore) : IMemberServices
    {
        public async Task<Member> Create(MemberDto member, CancellationToken cancellationToken = default)
        {
            var create = mapper.Map<Member>(member);
            create.Id = Guid.NewGuid();
            create.CreatedDate = DateTime.Now;

            await memberStore.Create(create,cancellationToken);

            return create;

        }

        public async Task<MemberDto> Get(Guid Id, CancellationToken cancellationToken = default)
        {
            var model = await memberStore.Get(Id,cancellationToken);

            return mapper.Map<MemberDto>(model);
        }

        public async Task<PagedResult<MemberResultDto>> Gets(GetMembers request, CancellationToken cancellationToken = default) 
        {

            var paging = PagedRequest.From(request.PageSize, request.PageNumber);

            var query = memberStore.Query();

            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.Trim();
                query = query.Where(x => EF.Functions.Like(x.Name, $"%{search}%"));
            }

            query = query.OrderBy(x => x.CreatedDate);

            var page = await query.PageResultAsync(paging, cancellationToken);

            var mapped = page.Items
              .Select(x => mapper.Map<MemberResultDto>(x))
              .ToList();

            return new PagedResult<MemberResultDto>(
                mapped,
                page.TotalCount,
                page.PageSize,
                page.PageNumber
            );

        }
    }
}
