using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lipip.Atomic.EntityFramework.Common.Authentications
{
    public interface ICurrentUser
    {
        string? UserId { get; }
        string? Username { get; }
    }
}
