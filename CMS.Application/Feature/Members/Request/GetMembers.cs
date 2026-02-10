using CMS.Application.Feature.Members.Dtos;
using Lipip.Atomic.EntityFramework.Core.Paginations;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Members.Request
{
    public class GetMembers : IRequest<IResult<PagedResult<MemberResultDto>>>
    {
        public string? Search { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}
