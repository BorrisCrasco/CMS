using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lipip.Atomic.EntityFramework.Common.Dtos
{
    public class UserTokenDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
