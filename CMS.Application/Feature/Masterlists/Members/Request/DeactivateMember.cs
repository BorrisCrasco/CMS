using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Masterlists.Members.Request
{
    public class DeactivateMember : IRequest<IResult<Guid>>
    {
        public Guid Id { get; set; }
    }
}
