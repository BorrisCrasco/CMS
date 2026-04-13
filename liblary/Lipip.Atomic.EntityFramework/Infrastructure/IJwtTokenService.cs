using Lipip.Atomic.EntityFramework.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lipip.Atomic.EntityFramework.Infrastructure
{
    public interface IJwtTokenService
    {
        string GenerateToken(UserTokenDto user);
    }
}
