using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThaiEvents.Models;

namespace ThaiEvents.Data
{
    public class Repository : IRepository
    {
        private List<EventViewModel> _events { get; set; }

        private ILogger _logger;

        public Repository(ILogger logger)
        {
            _events = new List<EventViewModel>();
            _logger = logger;
        }

        public bool CreateEvent(string title, string note, DateTime startDate, DateTime endDate, bool isAllDay)
        {
            var isSuccess = false;
            try
            {
                var eventItem = new EventViewModel()
                {
                    Id = new Guid(),
                    Title = title,
                    Note = note,
                    Details = new List<EventDetailViewModel>() {
                        new EventDetailViewModel() {
                            Id = 1,
                            StartDateTime = startDate,
                            EndDateTime = endDate,
                            IsAllDay = isAllDay
                          }
                    }
                };

                _events.Add(eventItem);
                isSuccess = true;
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message,e.StackTrace);
            }
            return isSuccess;
        }

        public bool EditEvent(EventViewModel eventItem)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEventDetail(Guid eventID, int eventDetailId)
        {
            var isSuccess = false;
            var item = _events.Where(w => w.Id == eventID)
                            .Select(s => s.Details.Where(x => x.Id == eventDetailId).FirstOrDefault())
                            .FirstOrDefault();
            if(item != null)
            {
                _events.Where(w => w.Id == eventID)
                    .FirstOrDefault().Details.Remove(item);
                isSuccess = true;
            }
            return isSuccess;
        }

        public bool CreateEventDetail(Guid id, DateTime startDate, DateTime endDate, bool isAllDay)
        {
            throw new NotImplementedException();
        }

        public bool EditEventDetail(Guid id, EventDetailViewModel eventItem)
        {
            throw new NotImplementedException();
        }

        public EventViewModel GetEvent(Guid Id, int eventDetailId = 0)
        {
            var result = _events.Where(w => w.Id == Id).FirstOrDefault();
            if(result != null && eventDetailId > 0)
            {
                var tmpItem = result.Details.Where(w => w.Id == eventDetailId).FirstOrDefault();
                result.Details = new List<EventDetailViewModel>() { tmpItem };
            }

            return result;
        }

        public bool DeleteEvent(Guid eventID)
        {
            var isSuccess = false;
            var item = _events.Where(w => w.Id == eventID).FirstOrDefault();
            if (item != null)
            {
                _events.Remove(item);
                isSuccess = true;
            }

            return isSuccess;
        }
    }
}
