using Cms.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Modules.Services
{
    public class ModuleStore(ChurchMSDBContext dbContext) : IModuleStore
    {
        public async Task Create(Module module, CancellationToken cancellation = default)
        {
            await dbContext.Modules.AddAsync(module, cancellation);
        }

        public async Task<Module?> Get(Guid id, CancellationToken cancellation = default)
        {
            var model = await dbContext.Modules
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellation) ?? null;

            return model;
        }

        public async Task<Module?> GetForUpdate(Guid id, CancellationToken cancellation = default)
        {
            var model = await dbContext.Modules
                .FirstOrDefaultAsync(x => x.Id == id, cancellation) ?? null;

            return model;
        }

        public async Task<IEnumerable<Module>> Gets(CancellationToken cancellation = default)
        {
            return await dbContext.Modules
                .AsNoTracking()
                .ToListAsync(cancellation);
        }

        public IQueryable<Module> Query()
        {
            return dbContext.Modules.AsNoTracking();
        }
    }
}
