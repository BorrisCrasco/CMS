using CMS.Application.Feature.Masterlists.Events.Dtos;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Masterlists.Events.Request
{
    public class CreateEvent : IRequest<IResult<Guid>>
    {
        [JsonIgnore]
        //public Guid Id { get; set; }
        public EventDto Event { get; set; }
    }
}
