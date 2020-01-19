using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThaiEvents.Models;
using ThaiEvents.Services;

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
     

        public bool CreateEvent(string title, string note, DateTime startDate, DateTime endDate, bool isAllDay, RecuringType recurType)
        {
            var isSuccess = false;
            try
            {
                if (isAllDay)
                {
                    
                    startDate = Utilities.ConvertStringToDateTime($"{DateTime.Now.ToString("dd/MM/yyyy")} 00:00");
                    endDate = Utilities.ConvertStringToDateTime($"{DateTime.Now.ToString("dd/MM/yyyy")} 23:59");
                }
                var eventItem = new EventViewModel()
                {
                    Id = Guid.NewGuid(),
                    Title = title,
                    Note = note,
                    recurType = recurType,
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
                results.AddRange(GetEventDisplayForNext30Year(eventItem));
                results.AddRange(GetEventDisplayForNext120Month(eventItem));
                results.AddRange(GetEventDisplayForNext200weeks(eventItem));
                results.AddRange(GetEventDisplayForNext365days(eventItem));
            }
            return results.OrderBy(w => w.StartDateTime).ToList();
        }

        private List<EventDisplayViewModel> GetEventDisplayForNext30Year(EventViewModel eventItem)
        {
            var result = new List<EventDisplayViewModel>();
            if (eventItem.recurType == RecuringType.Year)
            {
                var eventDetailItem = eventItem.Details.First();
                for (var y = 1; y <= 30;y++)
                {
                    result.Add(new EventDisplayViewModel() {
                                EventId = eventItem.Id.ToString(),
                                Title = eventItem.Title,
                                Note = eventItem.Note,
                                StartDateTime = eventDetailItem.StartDateTime.AddYears(y),
                                EndDateTime = eventDetailItem.EndDateTime.AddYears(y)
                    });

                }
            }
            return result;
        }
        private List<EventDisplayViewModel> GetEventDisplayForNext120Month(EventViewModel eventItem)
        {
            var result = new List<EventDisplayViewModel>();
            if (eventItem.recurType == RecuringType.Month)
            {
                var eventDetailItem = eventItem.Details.First();
                for (var m = 1; m <=  120; m++)
                {
                    result.Add(new EventDisplayViewModel()
                    {
                        EventId = eventItem.Id.ToString(),
                        Title = eventItem.Title,
                        Note = eventItem.Note,
                        StartDateTime = eventDetailItem.StartDateTime.AddMonths(m),
                        EndDateTime = eventDetailItem.EndDateTime.AddMonths(m)
                    });

                }
            }
            return result;
        }
        private List<EventDisplayViewModel> GetEventDisplayForNext365days(EventViewModel eventItem)
        {
            var result = new List<EventDisplayViewModel>();
            if (eventItem.recurType == RecuringType.Day)
            {
                var eventDetailItem = eventItem.Details.First();
                for (var d = 1; d <= 365; d++)
                {
                    result.Add(new EventDisplayViewModel()
                    {
                        EventId = eventItem.Id.ToString(),
                        Title = eventItem.Title,
                        Note = eventItem.Note,
                        StartDateTime = eventDetailItem.StartDateTime.AddDays(d),
                        EndDateTime = eventDetailItem.EndDateTime.AddDays(d)
                    });

                }
            }
            return result;
        }
        private List<EventDisplayViewModel> GetEventDisplayForNext200weeks(EventViewModel eventItem)
        {
            var result = new List<EventDisplayViewModel>();
            if (eventItem.recurType == RecuringType.Week)
            {
                var eventDetailItem = eventItem.Details.First();
                for (var w = 1; w <= 200; w++)
                {
                    result.Add(new EventDisplayViewModel()
                    {
                        EventId = eventItem.Id.ToString(),
                        Title = eventItem.Title,
                        Note = eventItem.Note,
                        StartDateTime = eventDetailItem.StartDateTime.AddDays((w*7)),
                        EndDateTime = eventDetailItem.EndDateTime.AddDays((w * 7))
                    });

                }
            }
            return result;
        }

        public void DeleteAll()
        {
            _events.RemoveAll(w=>w.Id != Guid.Empty);
        }
    }
}
