using Cms.Persistence.Models;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Users.Request
{
    public class ReactivateUser : IRequest<IResult<Guid>>
    {
        public Guid Id { get; set; }
    }
}
