using Cms.Persistence.Models;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Members.Services
{
    public class MemberStore(ChurchMSDBContext dbContext) : IMemberStore
    {
        public async Task Create(Member member, CancellationToken cancellation = default)
        {
            await dbContext.Members.AddAsync(member, cancellation);
        }

        public async Task<IEnumerable<Member>> Gets(CancellationToken cancellation = default)
        {
            return await dbContext.Members
                .AsNoTracking()
                .ToListAsync(cancellation);

        }

        public async Task<Member?> Get(Guid id, CancellationToken cancellation = default)
        {
            var model = await dbContext.Members
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellation) ?? null;

            return model;

        }

        public async Task<Member?> GetForUpdate(Guid id, CancellationToken cancellation = default)
        {
            var model = await dbContext.Members
                .FirstOrDefaultAsync(x => x.Id == id, cancellation) ?? null;

            return model;
        }


    }
}
