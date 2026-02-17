using CMS.Application.Feature.Masterlists.Events.Dtos;
using Lipip.Atomic.EntityFramework.Core.Paginations;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Masterlists.Events.Request
{
    public class GetEvents : IRequest<IResult<PagedResult<EventResultsDto>>>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
