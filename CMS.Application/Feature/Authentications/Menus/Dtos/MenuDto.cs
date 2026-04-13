using CMS.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Menus.Dtos
{
    public class MenuDto : AuditEntityDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public int Order { get; set; }
        public bool IsActive { get; set; }


    }
}
