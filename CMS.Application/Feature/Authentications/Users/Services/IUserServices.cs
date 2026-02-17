using Cms.Persistence.Models;
using CMS.Application.Feature.Authentications.Users.Dtos;
using CMS.Application.Feature.Authentications.Users.Request;
using CMS.Application.Feature.Masterlists.Members.Dtos;
using CMS.Application.Feature.Masterlists.Members.Request;
using Lipip.Atomic.EntityFramework.Core.Paginations;
using Lipip.Atomic.EntityFramework.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Users.Services
{
    public interface IUserServices
    {
        Task<IResult<UserDto>> Create(UserDto request, CancellationToken cancellationToken = default);

        Task<IResult<UserDto>> Get(Guid Id, CancellationToken cancellationToken = default);

        Task<PagedResult<UserResultDto>> GetPaged(GetUsersQuery request, CancellationToken cancellationToken = default);

        Task<IResult<UserDto>> Update(UserDto request, CancellationToken cancellationToken = default);

        Task<IResult<Guid>> Reactivate(Guid id, CancellationToken cancellationToken = default);

        Task<IResult<Guid>> Deactivate(Guid id, CancellationToken cancellationToken = default);

    }
}
