using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Modules.Dtos
{
    public class ModuleResultDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? MenuName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
