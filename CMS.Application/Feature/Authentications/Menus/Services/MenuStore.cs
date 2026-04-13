using Cms.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Feature.Authentications.Menus.Services
{
    public class MenuStore(ChurchMSDBContext dbContext) : IMenuStore
    {
        public async Task Create(Menu menu, CancellationToken cancellation = default)
        {
            await dbContext.Menus.AddAsync(menu, cancellation);
        }

        public async Task<Menu?> Get(Guid id, CancellationToken cancellation = default)
        {
            var model = await dbContext.Menus
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellation) ?? null;

            return model;
        }

        public async Task<Menu?> GetForUpdate(Guid id, CancellationToken cancellation = default)
        {
            var model = await dbContext.Menus
                .FirstOrDefaultAsync(x => x.Id == id, cancellation) ?? null;

            return model;
        }

        public async Task<IEnumerable<Menu>> Gets(CancellationToken cancellation = default)
        {
            return await dbContext.Menus
                .AsNoTracking()
                .ToListAsync(cancellation);
        }

        public IQueryable<Menu> Query()
        {
            return dbContext.Menus.AsNoTracking();
        }
    }
}
