using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThaiEvents.Models
{
    public class EventDisplayViewModel
    {
        public string EventId { get; set; }
        public int EventDetailId { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
