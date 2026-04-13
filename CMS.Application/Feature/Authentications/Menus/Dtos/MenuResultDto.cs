using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Authentications.Menus.Dtos
{
    public class MenuResultDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}
