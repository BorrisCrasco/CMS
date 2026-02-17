using Cms.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Roles.Services
{
    public interface IRoleStore
    {
        Task Create(Role role, CancellationToken cancellation = default);
        Task<IEnumerable<Role>> Gets(CancellationToken cancellation = default);
        IQueryable<Role> Query();
        Task<Role?> Get(int id, CancellationToken cancellation = default);
        Task<Role?> GetForUpdate(int id, CancellationToken cancellation = default);
    }
}
