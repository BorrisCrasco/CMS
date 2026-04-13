using Cms.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Modules.Services
{
    public interface IModuleStore
    {
        Task Create(Module module, CancellationToken cancellation = default);
        Task<IEnumerable<Module>> Gets(CancellationToken cancellation = default);
        IQueryable<Module> Query();
        Task<Module?> Get(Guid id, CancellationToken cancellation = default);
        Task<Module?> GetForUpdate(Guid id, CancellationToken cancellation = default);
    }
}
