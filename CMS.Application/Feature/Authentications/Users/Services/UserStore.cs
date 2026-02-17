using Cms.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Users.Services
{
    public class UserStore(ChurchMSDBContext dbContext) : IUserStore
    {
        public async Task Create(User request, CancellationToken cancellation = default)
        {
            await dbContext.Users.AddAsync(request, cancellation);
        }

        public async Task<VwUser?> Get(Guid id, CancellationToken cancellation = default)
        {
            var model = await dbContext.VwUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellation) ?? null;

            return model;
        }

        public async Task<User?> GetForUpdate(Guid id, CancellationToken cancellation = default)
        {
            var model = await dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == id, cancellation) ?? null;

            return model;
        }

        public async Task<IEnumerable<VwUser>> Gets(CancellationToken cancellation = default)
        {
            return await dbContext.VwUsers
                .AsNoTracking()
                .ToListAsync(cancellation);
        }

        public IQueryable<VwUser> Query()
        {
            return dbContext.VwUsers.AsNoTracking();
        }
    }
}
