using Cms.Persistence.Models;
using CMS.Application.Feature.Masterlists.Events.Dtos;
using CMS.Application.Feature.Masterlists.Events.Request;
using Lipip.Atomic.EntityFramework.Result;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Masterlists.Events.Handler
{
    public class GetEventHandler(ChurchMSDBContext DbContext, IMapper mapper)
        : IRequestHandler<GetEvent, IResult<EventDto>>
    {
        public async Task<IResult<EventDto>> Handle(GetEvent request, CancellationToken cancellationToken)
        {
            var model = await DbContext.Events
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            var dto = mapper.Map<EventDto>(model);

            return Result<EventDto>.Success(dto);

        }
    }
}
