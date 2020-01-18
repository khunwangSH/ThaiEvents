using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThaiEvents.Models;

namespace ThaiEvents.Data
{
    interface IRepository
    {
        bool CreateEvent(string title, string note, DateTime startDate, DateTime endDate, bool isAllDay);
        bool CreateEventDetail(Guid id, DateTime startDate, DateTime endDate, bool isAllDay);
        bool EditEvent(EventViewModel eventItem);
        bool EditEventDetail(Guid id, EventDetailViewModel eventItem);
        bool DeleteEvent(Guid eventID);
        bool DeleteEventDetail(Guid eventID, int eventDetailId);
        EventViewModel GetEvent(Guid Id, int eventDetailId = 0);
    }
}
