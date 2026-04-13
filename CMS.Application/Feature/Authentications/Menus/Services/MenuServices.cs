using Cms.Persistence.Models;
using CMS.Application.Feature.Authentications.Menus.Dtos;
using CMS.Application.Feature.Authentications.Menus.Request;
using CMS.Application.Feature.Authentications.Roles.Request;
using Lipip.Atomic.EntityFramework.Common.Paginations;
using Lipip.Atomic.EntityFramework.Result;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Feature.Authentications.Menus.Services
{
    public class MenuServices(IMenuStore menuStore, IMapper mapper) : IMenuServices
    {
        public async Task<IResult<MenuDto>> Create(MenuDto request, CancellationToken cancellationToken = default)
        {
            var create = mapper.Map<Menu>(request);
            create.IsActive = true;

            await menuStore.Create(create, cancellationToken);

            var result = mapper.Map<MenuDto>(create);

            return Result.Success(result);
        }

        public async Task<IResult<Guid>> Deactivate(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await menuStore.GetForUpdate(id, cancellationToken);

            if (model is null)
                return Result<Guid>.NotFound("Id not found!");

            model.IsActive = false;

            return Result.Success(model.Id);
        }

        public async Task<IResult<MenuDto>> Get(Guid Id, CancellationToken cancellationToken = default)
        {
            var model = await menuStore.Get(Id, cancellationToken);

            if (model is null)
                return Result<MenuDto>.NotFound("Id not found!");

            var result = mapper.Map<MenuDto>(model);

            return Result.Success(result);
        }

        public async Task<PagedResult<MenuResultDto>> GetPaged(GetMenuQuery request, CancellationToken cancellationToken = default)
        {
            var paging = PagedRequest.From(request.PageNumber, request.PageSize);

            var query = menuStore.Query();

            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.Trim();
                query = query.Where(x => EF.Functions.Like(x.Name, $"%{search}%"));
            }

            var page = await query.PageResultAsync(paging, cancellationToken);

            var mapped = page.Items
              .Select(x => mapper.Map<MenuResultDto>(x))
              .ToList();

            return new PagedResult<MenuResultDto>(
                mapped,
                page.TotalCount,
                page.PageNumber,
                page.PageSize
            );
        }

        public async Task<bool> MenuExist(Guid id, CancellationToken cancellationToken)
        {
            var model = await menuStore.Get(id, cancellationToken);

            return model is null ? false : true;
        }

        public async Task<IResult<Guid>> Reactivate(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await menuStore.GetForUpdate(id, cancellationToken);
            if (model is null)
                return Result<Guid>.NotFound("Id not found!");

            model.IsActive = true;

            return Result.Success(model.Id);
        }

        public async Task<IResult<MenuDto>> Update(MenuDto request, CancellationToken cancellationToken = default)
        {
            var model = await menuStore.GetForUpdate(request.Id, cancellationToken);
            if (model is null)
                return Result<MenuDto>.NotFound("Id not found!");

            var dto = mapper.Map(request, model);
            dto.UpdatedDate = DateTime.Now;

            var result = mapper.Map<MenuDto>(model);

            return Result.Success(result);
        }
    }
}
