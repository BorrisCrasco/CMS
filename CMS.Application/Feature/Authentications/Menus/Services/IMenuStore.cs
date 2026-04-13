using Cms.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Menus.Services
{
    public interface IMenuStore
    {
        Task Create(Menu menu, CancellationToken cancellation = default);
        Task<IEnumerable<Menu>> Gets(CancellationToken cancellation = default);
        IQueryable<Menu> Query();
        Task<Menu?> Get(Guid id, CancellationToken cancellation = default);
        Task<Menu?> GetForUpdate(Guid id, CancellationToken cancellation = default);
    }
}
