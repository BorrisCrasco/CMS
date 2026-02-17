using Cms.Persistence.Models;
using CMS.Application.Feature.Masterlists.Members.Dtos;
using CMS.Application.Feature.Masterlists.Members.Request;
using Lipip.Atomic.EntityFramework.Core.Paginations;
using Lipip.Atomic.EntityFramework.Result;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Masterlists.Members.Services
{
    public class MemberServices(IMapper mapper, IMemberStore memberStore) : IMemberServices
    {
        public async Task<IResult<Member>> Create(MemberDto member, CancellationToken cancellationToken = default)
        {
           
            var genderExist = await GenderExist(member.GenderId, cancellationToken);

            if(!genderExist)
                return Result<Member>.NotFound("Gender id not found!");

            var create = mapper.Map<Member>(member);
            create.Id = Guid.NewGuid();
            create.CreatedDate = DateTime.Now;

            await memberStore.Create(create,cancellationToken);

            return Result.Success(create);

        }

        public async Task<IResult<MemberDto>> Get(Guid Id, CancellationToken cancellationToken = default)
        {
            var model = await memberStore.Get(Id,cancellationToken);
            if(model is null)
                return Result<MemberDto>.NotFound("Id not found!");

            var dto = mapper.Map<MemberDto>(model);

            return Result.Success(dto);
        }

        public async Task<PagedResult<MemberResultDto>> GetPaged(GetMembersQuery request, CancellationToken cancellationToken = default) 
        {

            var paging = PagedRequest.From(request.PageNumber, request.PageSize);

            var query = memberStore.Query();

            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.Trim();
                query = query.Where(x => EF.Functions.Like(x.Name, $"%{search}%"));
            }

            var page = await query.PageResultAsync(paging, cancellationToken);

            var mapped = page.Items
              .Select(x => mapper.Map<MemberResultDto>(x))
              .ToList();

            return new PagedResult<MemberResultDto>(
                mapped,
                page.TotalCount,
                page.PageNumber,
                page.PageSize
            );

        }


        public async Task<IResult<Member>> Update(MemberDto member, CancellationToken cancellationToken = default)
        {
            var genderExist = await GenderExist(member.GenderId, cancellationToken);

            if (!genderExist)
                return Result<Member>.NotFound("Gender id not found!");

            var model = await memberStore.GetForUpdate(member.Id, cancellationToken);
            if (model is null)
                return Result<Member>.NotFound("Id not found!");

            var dto = mapper.Map(member, model);
            dto.UpdatedDate = DateTime.Now;

            return Result.Success(dto);

        }
        public async Task<IResult<Guid>> DeactivateMember(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await memberStore.GetForUpdate(id, cancellationToken);
            if (model is null)
                return Result<Guid>.NotFound("Id not found!");

            model.IsActive = false;

            return Result.Success(model.Id);
        }

        public async Task<IResult<Guid>> ReactivateMember(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await memberStore.GetForUpdate(id, cancellationToken);
            if (model is null)
                return Result<Guid>.NotFound("Id not found!");

            model.IsActive = true;

            return Result.Success(model.Id);
        }

        public async Task<IResult<IEnumerable<GenderDto>>> GetGenders(CancellationToken cancellationToken = default)
        {
            var query = await memberStore.GetGenders();

            var dtos = mapper.Map<IEnumerable<GenderDto>>(query);

            return Result.Success(dtos);
           
        }

        public async Task<bool> GenderExist(int id, CancellationToken cancellationToken)
        {
            var model = await memberStore.GetGender(id, cancellationToken);

            return model is null ? false : true;

        }
    }
}
