using Cms.Persistence.Models;
using CMS.Application.Common.Authentications;
using CMS.Application.Feature.Authentications.Roles.Services;
using CMS.Application.Feature.Authentications.Users.Dtos;
using CMS.Application.Feature.Authentications.Users.Request;
using Lipip.Atomic.EntityFramework.Common.Authentications;
using Lipip.Atomic.EntityFramework.Common.Dtos;
using Lipip.Atomic.EntityFramework.Core.Paginations;
using Lipip.Atomic.EntityFramework.Result;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Feature.Authentications.Users.Services
{
    public class UserServices(IUserStore userStore, IMapper mapper, IRoleServices roleServices , IJwtTokenService jwtToken) : IUserServices
    {
        public async Task<IResult<UserDto>> Create(UserDto request, CancellationToken cancellationToken = default)
        {
            var roleExist = await roleServices.RoleExist(request.RoleId, cancellationToken);

            if (!roleExist)
                return Result<UserDto>.NotFound("Role id not found!");

            if (string.IsNullOrWhiteSpace(request.Password))
                return Result<UserDto>.BadRequest("Password is required");

            PasswordHasher.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var create = mapper.Map<User>(request);
            create.Id = Guid.NewGuid();
            create.PasswordHash = passwordHash;
            create.PasswordSalt = passwordSalt;
            create.CreatedDate = DateTime.UtcNow;

            await userStore.Create(create, cancellationToken);

            return Result.Success(request);
        }

        public async Task<IResult<Guid>> Deactivate(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await userStore.GetForUpdate(id, cancellationToken);
            if (model is null)
                return Result<Guid>.NotFound("Id not found!");

            model.IsActive = false;

            return Result.Success(model.Id);
        }

        public async Task<IResult<UserDto>> Get(Guid Id, CancellationToken cancellationToken = default)
        {
            var model = await userStore.Get(Id, cancellationToken);
            if (model is null)
                return Result<UserDto>.NotFound("Id not found!");

            var dto = mapper.Map<UserDto>(model);

            return Result.Success(dto);
        }

        public async Task<PagedResult<UserResultDto>> GetPaged(GetUsersQuery request, CancellationToken cancellationToken = default)
        {

            var paging = PagedRequest.From(request.PageNumber, request.PageSize);

            var query = userStore.Query();

            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.Trim();
                query = query.Where(x => EF.Functions.Like(x.Username, $"%{search}%"));
            }

            var page = await query.PageResultAsync(paging, cancellationToken);

            var mapped = page.Items
              .Select(x => mapper.Map<UserResultDto>(x))
              .ToList();

            return new PagedResult<UserResultDto>(
                mapped,
                page.TotalCount,
                page.PageNumber,
                page.PageSize
            );
        }

        public async Task<IResult<Guid>> Reactivate(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await userStore.GetForUpdate(id, cancellationToken);
            if (model is null)
                return Result<Guid>.NotFound("Id not found!");

            model.IsActive = true;

            return Result.Success(model.Id);
        }

        public async Task<IResult<UserDto>> Update(UserDto request, CancellationToken cancellationToken = default)
        {
            var roleExist = await roleServices.RoleExist(request.RoleId, cancellationToken);

            if (!roleExist)
                return Result<UserDto>.NotFound("Role id not found!");

            var model = await userStore.GetForUpdate(request.Id, cancellationToken);
            if (model is null)
                return Result<UserDto>.NotFound("Id not found!");

            var dto = mapper.Map(request, model);
            dto.UpdatedDate = DateTime.Now;

            return Result.Success(request);
        }

        public async Task<IResult<UserAuthenticationDto>> Login(LoginUser request, CancellationToken cancellationToken = default)
        {
            var model = await GetByUsername(request.Username, cancellationToken);

            if (model is null)
                return Result<UserAuthenticationDto>.Unauthorized("Invalid username and password!");

            var valid = PasswordHasher.VerifyPassword
             (
                request.Password!,
                model.PasswordHash,
                model.PasswordSalt
             );

            if (!valid)
                return Result<UserAuthenticationDto>.Unauthorized("Invalid username or password!");

            var role = model.RoleName;

            var token = new UserTokenDto
            {
                Id = model.Id,
                Username = model.Username,
                Role = role,
            };

            var generateToken = jwtToken.GenerateToken(token);

            return Result.Success(new UserAuthenticationDto
            {
                Id = model.Id,
                Username = model.Username,
                RoleId = model.RoleId,
                RoleName = role,
                Token = generateToken
            });


        }


        public async Task<VwUser?> GetByUsername(string username, CancellationToken cancellationToken = default)
        {
            var model = await userStore.Gets();

            var validate = model
                .Where(x => x.Username == username && x.IsActive == true)
                .SingleOrDefault() ?? null;

            return validate;
        }


    }
}
