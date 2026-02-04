using Cms.Persistence.Models;
using CMS.Application.Feature.Events.Request;
using Lipip.Atomic.EntityFramework.Result;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Events.Handler
{
    public class CreateEventHandler(ChurchMSDBContext dbContext, IMapper mapper)
        : IRequestHandler<CreateEvent, IResult<Guid>>
    {

        public async Task<IResult<Guid>> Handle(CreateEvent request, CancellationToken cancellationToken)
        {

            var validation = await ErrorHandler(request, cancellationToken);

            if (!validation.IsSuccess)
            {
                return validation;  
            }

            var model = mapper.Map<Event>(request.Event);
            model.Id = Guid.NewGuid();
            model.CreatedAt = DateTime.Now; 

            await dbContext.Events.AddAsync(model, cancellationToken);

            return Result<Guid>.Success(model.Id);

        }

        private async Task<IResult<Guid>> ErrorHandler (CreateEvent request , CancellationToken cancellationToken)
        {
            var eventExist = await dbContext.Events
                .AnyAsync(x => x.Description == request.Event.Description);

            if (eventExist)
                return Result<Guid>.BadRequest("Event already exist!");
            

            return Result<Guid>.Success(Guid.Empty);
        }
    }
}
