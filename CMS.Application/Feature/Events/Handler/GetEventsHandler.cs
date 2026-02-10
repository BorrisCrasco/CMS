using Cms.Persistence.Models;
using CMS.Application.Feature.Events.Dtos;
using CMS.Application.Feature.Events.Request;
using Lipip.Atomic.EntityFramework.Core.Paginations;
using Lipip.Atomic.EntityFramework.Result;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Events.Handler
{
    public class GetEventsHandler(ChurchMSDBContext context, IMapper mapper)
        : IRequestHandler<GetEvents, IResult<PagedResult<EventResultsDto>>>
    {
        //public async Task<IResult<PagedResult<EventResultsDto>>> Handle(GetEvents request, CancellationToken cancellationToken)
        //{
        //    //var paging = PagedRequest.From(request.Skip, request.Take);

        //    //var query = context.Events
        //    //    .AsNoTracking()
        //    //    .ProjectToType<EventResultsDto>();    


        //    //var page = await query.PageResultAsync(paging, cancellationToken);

        //    //return Result<PagedResult<EventResultsDto>>.Success(page);

        //}
        public Task<IResult<PagedResult<EventResultsDto>>> Handle(GetEvents request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
