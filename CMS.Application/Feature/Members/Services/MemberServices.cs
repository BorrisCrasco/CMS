using Cms.Persistence.Models;
using CMS.Application.Feature.Members.Dtos;
using MapsterMapper;
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
    }
}
