using Cms.Persistence.Models;
using CMS.Application.Feature.Masterlists.Members.Dtos;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Masterlists.Members.Request
{
    public class UpdateMember : IRequest<IResult<Member>>
    {
        public MemberDto Member { get; set; }
    }
}
