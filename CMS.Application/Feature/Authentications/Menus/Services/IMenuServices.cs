using CMS.Application.Feature.Authentications.Menus.Dtos;
using CMS.Application.Feature.Authentications.Menus.Request;
using Lipip.Atomic.EntityFramework.Common.Paginations;
using Lipip.Atomic.EntityFramework.Result;

namespace CMS.Application.Feature.Authentications.Menus.Services
{
    public interface IMenuServices
    {
        Task<IResult<MenuDto>> Create(MenuDto request, CancellationToken cancellationToken = default);

        Task<IResult<MenuDto>> Get(Guid Id, CancellationToken cancellationToken = default);

        Task<PagedResult<MenuResultDto>> GetPaged(GetMenuQuery request, CancellationToken cancellationToken = default);

        Task<IResult<MenuDto>> Update(MenuDto request, CancellationToken cancellationToken = default);

        Task<IResult<Guid>> Reactivate(Guid id, CancellationToken cancellationToken = default);

        Task<IResult<Guid>> Deactivate(Guid id, CancellationToken cancellationToken = default);

        Task<bool> MenuExist(Guid id, CancellationToken cancellationToken);
    }
}
