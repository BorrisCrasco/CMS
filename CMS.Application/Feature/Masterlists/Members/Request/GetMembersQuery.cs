using CMS.Application.Feature.Masterlists.Members.Dtos;
using Lipip.Atomic.EntityFramework.Core.Paginations;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Masterlists.Members.Request
{
    public class GetMembersQuery : IRequest<PagedResult<MemberResultDto>>
    {
        public string? Search { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

    }
}
