using Cms.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Users.Services
{
    public interface IUserStore
    {
        Task Create(User request, CancellationToken cancellation = default);
        Task<IEnumerable<VwUser>> Gets(CancellationToken cancellation = default);
        IQueryable<VwUser> Query();
        Task<VwUser?> Get(Guid id, CancellationToken cancellation = default);
        Task<User?> GetForUpdate(Guid id, CancellationToken cancellation = default);

    }
}
