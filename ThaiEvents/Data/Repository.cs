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

        private ILogger<Repository> _logger;
    
        public Repository(ILogger<Repository> logger)
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
            var isSuccess = false;
            try
            {

            }catch(Exception e)
            {
                _logger.LogError(e.Message,e.StackTrace);
            }

            return isSuccess;
        }

        public bool DeleteEventDetail(Guid eventID, int eventDetailId)
        {
            var isSuccess = false;
            //var item = _events.Where(w => w.Id == eventID)
            //                .Select(s => s.Details.Where(x => x.Id == eventDetailId).FirstOrDefault())
            //                .FirstOrDefault();
            //if(item != null)
            //{
            //    _events.Where(w => w.Id == eventID)
            //        .FirstOrDefault().Details.Remove(item);
            //    isSuccess = true;
            //}
            var item = _events.Where(w => w.Id == eventID).FirstOrDefault();
            if (item != null)
            {
                var result = _events.RemoveAll(w => w.Id == eventID);
                 result += item.Details.RemoveAll(x => x.Id == eventDetailId);
                _events.Add(item);
                isSuccess = result > 0;
            }
            return isSuccess;
        }

        public bool CreateEventDetail(Guid id, DateTime startDate, DateTime endDate, bool isAllDay)
        {
            var isSucess = false;
            try
            {

                var item = new EventDetailViewModel()
                {
                    StartDateTime = startDate,
                    EndDateTime = endDate,
                    IsAllDay = isAllDay
                };
                _events.Where(w => w.Id == id).First().Details.Add(item);
                //var eventItem = _events.Where(w => w.Id == id).First().Details.Add(item);
                //var indx = _events.IndexOf(eventItem);
                //_events[indx].Details.Add(item);
                isSucess = true;
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message,e.StackTrace);
            }

            return isSucess;
        }

        public bool EditEventDetail(Guid id, EventDetailViewModel eventItem)
        {
            var isSuccess = false;
            try
            {

            }catch(Exception e)
            {

            }

            return isSuccess;
        }

        public EventViewModel GetEvent(Guid Id, int eventDetailId = 0)
        {
            var result = _events.Where(w => w.Id == Id).FirstOrDefault();
            try
            {

                if (result != null && eventDetailId > 0)
                {
                    var tmpItem = result.Details.Where(w => w.Id == eventDetailId).First();
                    result.Details = new List<EventDetailViewModel>() { tmpItem };
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message,e.StackTrace);
            }

            return result;
        }

        public bool DeleteEvent(Guid eventID)
        {
            var isSuccess = false;
            var item = _events.RemoveAll(w => w.Id == eventID);

            return isSuccess;
        }

        public List<EventDisplayViewModel> GetEvents()
        {
            var results = new List<EventDisplayViewModel>();
            foreach (var eventItem in _events)
            {
                foreach (var eventDetailItem in eventItem.Details)
                {
                    results.Add(new EventDisplayViewModel() { 
                                    EventId = eventItem.Id.ToString(),
                                    Title = eventItem.Title,
                                    Note = eventItem.Note,
                                    StartDateTime = eventDetailItem.StartDateTime,
                                    EndDateTime = eventDetailItem.EndDateTime
                            });
                }
            }
            return results;
        }
    }
}
