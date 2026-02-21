using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Users.Dtos
{
    public class UserAuthenticationDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string? Password { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public string Token { get; set; } = null!;
    }
}
