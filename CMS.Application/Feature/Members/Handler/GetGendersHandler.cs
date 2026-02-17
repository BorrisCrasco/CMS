using CMS.Application.Feature.Members.Dtos;
using CMS.Application.Feature.Members.Request;
using CMS.Application.Feature.Members.Services;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Members.Handler
{
    public class GetGendersHandler(IMemberServices memberServices) : IRequestHandler<GetGenders, IResult<IEnumerable<GenderDto>>>
    {
        public async Task<IResult<IEnumerable<GenderDto>>> Handle(GetGenders request, CancellationToken cancellationToken)
        {
            return await memberServices.GetGenders(cancellationToken);
        }
    }
}
