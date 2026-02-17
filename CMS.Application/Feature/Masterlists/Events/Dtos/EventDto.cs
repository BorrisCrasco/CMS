using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Masterlists.Events.Dtos
{
    public class EventDto
    {
        public Guid? Id { get; set; }
        public string Description { get; set; }

        public DateTime EventDate { get; set; }

        public bool IsOneTime { get; set; }

        public bool? IsDayPassed { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
