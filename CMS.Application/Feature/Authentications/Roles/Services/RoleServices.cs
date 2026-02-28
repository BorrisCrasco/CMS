using Cms.Persistence.Models;
using CMS.Application.Feature.Authentications.Roles.Dtos;
using CMS.Application.Feature.Authentications.Roles.Request;
using CMS.Application.Feature.Masterlists.Members.Dtos;
using CMS.Application.Feature.Masterlists.Members.Services;
using Lipip.Atomic.EntityFramework.Common.Paginations;
using Lipip.Atomic.EntityFramework.Result;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Roles.Services
{
    public class RoleServices(IMapper mapper, IRoleStore roleStore) : IRoleServices
    {
        public async Task<IResult<RoleDto>> Create(RoleDto request, CancellationToken cancellationToken = default)
        {
            var create = mapper.Map<Role>(request);
            //create.CreatedDate = DateTime.Now;

            await roleStore.Create(create, cancellationToken);

            return Result.Success(request);
        }

        public async Task<IResult<int>> Deactivate(int id, CancellationToken cancellationToken = default)
        {
            var model = await roleStore.GetForUpdate(id, cancellationToken);
            if (model is null)
                return Result<int>.NotFound("Id not found!");

            model.IsActive = false;

            return Result.Success(model.Id);
        }

        public async Task<IResult<RoleDto>> Get(int Id, CancellationToken cancellationToken = default)
        {
            var model = await roleStore.Get(Id, cancellationToken);
            if (model is null)
                return Result<RoleDto>.NotFound("Id not found!");

            var dto = mapper.Map<RoleDto>(model);

            return Result.Success(dto);
        }

        public async Task<PagedResult<RoleResultDto>> GetPaged(GetRolesQuery request, CancellationToken cancellationToken = default)
        {
            var paging = PagedRequest.From(request.PageNumber, request.PageSize);

            var query = roleStore.Query();

            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.Trim();
                query = query.Where(x => EF.Functions.Like(x.Name, $"%{search}%"));
            }

            var page = await query.PageResultAsync(paging, cancellationToken);

            var mapped = page.Items
              .Select(x => mapper.Map<RoleResultDto>(x))
              .ToList();

            return new PagedResult<RoleResultDto>(
                mapped,
                page.TotalCount,
                page.PageNumber,
                page.PageSize
            );
        }

        public async Task<IResult<int>> Reactivate(int id, CancellationToken cancellationToken = default)
        {
            var model = await roleStore.GetForUpdate(id, cancellationToken);
            if (model is null)
                return Result<int>.NotFound("Id not found!");

            model.IsActive = true;

            return Result.Success(model.Id);
        }


        public async Task<IResult<RoleDto>> Update(RoleDto request, CancellationToken cancellationToken = default)
        {
            var model = await roleStore.GetForUpdate(request.Id, cancellationToken);
            if (model is null)
                return Result<RoleDto>.NotFound("Id not found!");

            var dto = mapper.Map(request, model);
            dto.UpdatedDate = DateTime.Now;

            return Result.Success(request);
        }

        public async Task<bool> RoleExist(int id, CancellationToken cancellationToken)
        {
            var model = await roleStore.Get(id, cancellationToken);

            return model is null ? false : true;
        }
    }
}
