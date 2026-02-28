using CMS.Application.Feature.Authentications.Users.Dtos;
using Lipip.Atomic.EntityFramework.Common.Paginations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Users.Request
{
    public class GetUsersQuery : IRequest<PagedResult<UserResultDto>>
    {
        public string? Search { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
