using Cms.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Roles.Services
{
    public class RoleStore(ChurchMSDBContext dbContext) : IRoleStore
    {
        public async Task Create(Role role, CancellationToken cancellation = default)
        {
            await dbContext.Roles.AddAsync(role, cancellation);
        }

        public async Task<Role?> Get(int id, CancellationToken cancellation = default)
        {
            var model = await dbContext.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellation) ?? null;

            return model;
        }

        public async Task<Role?> GetForUpdate(int id, CancellationToken cancellation = default)
        {
            var model = await dbContext.Roles
                .FirstOrDefaultAsync(x => x.Id == id, cancellation) ?? null;

            return model;
        }

        public async Task<IEnumerable<Role>> Gets(CancellationToken cancellation = default)
        {
            return await dbContext.Roles
                .AsNoTracking()
                .ToListAsync(cancellation);
        }

        public IQueryable<Role> Query()
        {
            return dbContext.Roles.AsNoTracking();
        }
    }
}
