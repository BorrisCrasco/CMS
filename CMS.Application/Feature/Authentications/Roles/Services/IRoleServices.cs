using Cms.Persistence.Models;
using CMS.Application.Feature.Authentications.Roles.Dtos;
using CMS.Application.Feature.Authentications.Roles.Request;
using CMS.Application.Feature.Masterlists.Members.Dtos;
using CMS.Application.Feature.Masterlists.Members.Request;
using Lipip.Atomic.EntityFramework.Core.Paginations;
using Lipip.Atomic.EntityFramework.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Roles.Services
{
    public interface IRoleServices
    {
        Task<IResult<RoleDto>> Create(RoleDto request, CancellationToken cancellationToken = default);

        Task<IResult<RoleDto>> Get(int Id, CancellationToken cancellationToken = default);

        Task<PagedResult<RoleResultDto>> GetPaged(GetRolesQuery request, CancellationToken cancellationToken = default);

        Task<IResult<RoleDto>> Update(RoleDto request, CancellationToken cancellationToken = default);

        Task<IResult<int>> Reactivate(int id, CancellationToken cancellationToken = default);

        Task<IResult<int>> Deactivate(int id, CancellationToken cancellationToken = default);

        Task<bool> RoleExist(int id, CancellationToken cancellationToken);
    }
}
