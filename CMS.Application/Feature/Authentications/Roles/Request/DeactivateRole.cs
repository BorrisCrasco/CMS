using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Roles.Request
{
    public class DeactivateRole : IRequest<IResult<int>>
    {
        public int Id { get; set; } 
    }
}
