using CMS.Application.Feature.Events.Dtos;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Events.Request
{
    public class GetEvent : IRequest<IResult<EventDto>>
    {
        public Guid Id { get; set; }
    }
}
