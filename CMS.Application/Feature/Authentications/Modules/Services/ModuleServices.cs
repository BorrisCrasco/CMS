using Cms.Persistence.Models;
using CMS.Application.Feature.Authentications.Menus.Dtos;
using CMS.Application.Feature.Authentications.Menus.Services;
using CMS.Application.Feature.Authentications.Modules.Dtos;
using CMS.Application.Feature.Authentications.Modules.Request;
using CMS.Application.Feature.Authentications.Roles.Services;
using CMS.Application.Feature.Authentications.Users.Dtos;
using CMS.Application.Feature.Authentications.Users.Services;
using Lipip.Atomic.EntityFramework.Common.Paginations;
using Lipip.Atomic.EntityFramework.Result;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Modules.Services
{
    public class ModuleServices(
        IModuleStore moduleStore, 
        IMapper mapper,
        IMenuServices menuServices) : IModuleServices
    {
        public async Task<IResult<ModuleDto>> Create(ModuleDto request, CancellationToken cancellationToken = default)
        {

            var menuExist = await menuServices.MenuExist(request.MenuId, cancellationToken);

            if (!menuExist)
                return Result<ModuleDto>.NotFound("Menu id not found!");

            var create = mapper.Map<Module>(request);
            create.Id = Guid.NewGuid();
            create.IsActive = true;

            await moduleStore.Create(create, cancellationToken);

            var result = mapper.Map<ModuleDto>(create);

            return Result.Success(result);
        }

        public async Task<IResult<Guid>> Deactivate(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await moduleStore.GetForUpdate(id, cancellationToken);
            if (model is null)
                return Result<Guid>.NotFound("Id not found!");

            model.IsActive = false;

            return Result.Success(model.Id);
        }

        public async Task<IResult<ModuleDto>> Get(Guid Id, CancellationToken cancellationToken = default)
        {
            var model = await moduleStore.Get(Id, cancellationToken);
            if (model is null)
                return Result<ModuleDto>.NotFound("Id not found!");

            var result = mapper.Map<ModuleDto>(model);

            return Result.Success(result);
        }

        public async Task<PagedResult<ModuleResultDto>> GetPaged(GetModulesQuery request, CancellationToken cancellationToken = default)
        {
            var paging = PagedRequest.From(request.PageNumber, request.PageSize);

            var query = moduleStore.Query();

            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.Trim();
                query = query.Where(x => EF.Functions.Like(x.Name, $"%{search}%"));
            }

            var page = await query.PageResultAsync(paging, cancellationToken);

            var mapped = page.Items
              .Select(x => mapper.Map<ModuleResultDto>(x))
              .ToList();

            return new PagedResult<ModuleResultDto>(
                mapped,
                page.TotalCount,
                page.PageNumber,
                page.PageSize
            );
        }

        public async Task<bool> ModuleExist(Guid id, CancellationToken cancellationToken)
        {
            var model = await moduleStore.Get(id, cancellationToken);

            return model is null ? false : true;
        }

        public async Task<IResult<Guid>> Reactivate(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await moduleStore.GetForUpdate(id, cancellationToken);
            if (model is null)
                return Result<Guid>.NotFound("Id not found!");

            model.IsActive = true;

            return Result.Success(model.Id);
        }

        public async Task<IResult<ModuleDto>> Update(ModuleDto request, CancellationToken cancellationToken = default)
        {

            var model = await moduleStore.GetForUpdate(request.Id, cancellationToken);
            if (model is null)
                return Result<ModuleDto>.NotFound("Id not found!");

            var menuExist = await menuServices.MenuExist(request.MenuId, cancellationToken);

            if (!menuExist)
                return Result<ModuleDto>.NotFound("Menu id not found!");

            var dto = mapper.Map(request, model);

            var result = mapper.Map<ModuleDto>(model);

            return Result.Success(result);
        }
    }
}
