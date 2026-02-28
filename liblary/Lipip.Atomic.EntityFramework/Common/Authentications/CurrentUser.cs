using Microsoft.AspNetCore.Http;
using Lipip.Atomic.EntityFramework.Common.Authentications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Lipip.Atomic.EntityFramework.Common.Authentications
{
    public class CurrentUser(IHttpContextAccessor accessor) : ICurrentUser
    {
        public string? UserId => accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public string? Username => accessor.HttpContext?.User?
            .FindFirstValue(ClaimTypes.Name);
    }
}
