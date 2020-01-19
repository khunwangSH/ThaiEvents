using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThaiEvents.Models
{
    public class EventDisplayViewModel
    {
        public string EventId { get; set; }
        public int EventDetailId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Note { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDateTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndDateTime { get; set; }
        [Required]
        public int recurType { get; set; }
        public bool IsAllDay { get; set; }
    }
}
