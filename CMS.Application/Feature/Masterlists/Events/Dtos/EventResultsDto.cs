using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Feature.Masterlists.Events.Dtos
{
    public class EventResultsDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public bool? IsDayPassed { get; set; }
    }
}
