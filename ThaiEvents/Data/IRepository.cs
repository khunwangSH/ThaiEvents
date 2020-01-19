using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThaiEvents.Models;

namespace ThaiEvents.Data
{
    public interface IRepository
    {
        bool CreateEvent(string title, string note, DateTime startDate, DateTime endDate, bool isAllDay, RecuringType recurType);
        bool CreateEventDetail(Guid id, DateTime startDate, DateTime endDate, bool isAllDay);
        EventViewModel GetEvent(Guid Id, int eventDetailId = 0);
        List<EventDisplayViewModel> GetEvents();
        void DeleteAll();
    }
}
