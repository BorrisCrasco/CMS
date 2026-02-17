using Cms.Persistence.Models;
using CMS.Application.Feature.Masterlists.Members.Dtos;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Masterlists.Members.Request
{
    public class CreateMember : IRequest<IResult<Member>>
    {
        public MemberDto Member {  get; set; }
    }
}
