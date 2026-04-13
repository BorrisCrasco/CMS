using CMS.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Modules.Dtos
{
    public class ModuleDto : AuditEntityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid MenuId { get; set; }
        public string? MenuName {  get; set; }
        public string? Path { get; set; }
        public bool IsActive { get; set; }

    }
}
