using Cms.Persistence.Models;
using CMS.Application.Feature.Authentications.Modules.Dtos;
using CMS.Application.Feature.Authentications.Modules.Request;
using CMS.Application.Feature.Authentications.Users.Dtos;
using CMS.Application.Feature.Authentications.Users.Request;
using Lipip.Atomic.EntityFramework.Common.Paginations;
using Lipip.Atomic.EntityFramework.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Modules.Services
{
    public interface IModuleServices
    {
        Task<IResult<ModuleDto>> Create(ModuleDto request, CancellationToken cancellationToken = default);

        Task<IResult<ModuleDto>> Get(Guid Id, CancellationToken cancellationToken = default);

        Task<PagedResult<ModuleResultDto>> GetPaged(GetModulesQuery request, CancellationToken cancellationToken = default);

        Task<IResult<ModuleDto>> Update(ModuleDto request, CancellationToken cancellationToken = default);

        Task<IResult<Guid>> Reactivate(Guid id, CancellationToken cancellationToken = default);

        Task<IResult<Guid>> Deactivate(Guid id, CancellationToken cancellationToken = default);

        Task<bool> ModuleExist(Guid id, CancellationToken cancellationToken);

    }
}
