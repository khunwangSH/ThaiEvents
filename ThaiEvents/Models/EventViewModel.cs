using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThaiEvents.Models
{
    public class EventViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public List<EventDetailViewModel> Details { get; set; }
    }
}
