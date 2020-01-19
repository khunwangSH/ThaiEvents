using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThaiEvents.Data;
using ThaiEvents.Models;
using ThaiEvents.Services;

namespace ThaiEvents.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repos;
        private const int PAGE_SIZE = 5;

        public HomeController(ILogger<HomeController> logger,
                              IRepository repos)
        {
            _logger = logger;
            _repos = repos;
        }

        public IActionResult Index(string search="", int curPage =1)
        {
            //_repos.CreateEvent("Title Test","Event Note",DateTime.UtcNow,DateTime.UtcNow.AddHours(2),false,RecuringType.None);
            //_repos.CreateEvent("Title Test2", "Event Note2", DateTime.UtcNow.AddMonths(2), DateTime.UtcNow.AddMonths(2).AddHours(2), false, RecuringType.None);
            var s = search.ToLower();
            List<EventDisplayViewModel> items = new List<EventDisplayViewModel>();
            if (!String.IsNullOrEmpty(s))
                items = _repos.GetEvents().Where(w => w.Title.ToLower().Contains(s) || w.Note.ToLower().Contains(s)).ToList();
            else
                items = _repos.GetEvents();

            var pager = new Pager(items.Count,pageSize:PAGE_SIZE,currentPage:curPage);
            ViewBag.Pager = pager;
            ViewBag.search = search;
            return View((items.Count > PAGE_SIZE)? items.Skip((curPage-1) * PAGE_SIZE).Take(PAGE_SIZE).ToList() : items);
        }

        [HttpPost]
        public IActionResult Index(string search)
        {
            var s = search.ToLower();
            var items = _repos.GetEvents().Where(w => w.Title.ToLower().Contains(s) || w.Note.ToLower().Contains(s)).ToList();
            var pager = new Pager(items.Count, pageSize: PAGE_SIZE);
            ViewBag.Pager = pager;
            ViewBag.search = search;
            return View((items.Count > PAGE_SIZE) ? items.Take(PAGE_SIZE).ToList() : items);
        }

        public IActionResult CreateEvent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateEvent(EventDisplayViewModel model, string StartDateTime, string EndDateTime)
        {
           
            model.StartDateTime = Utilities.ConvertStringToDateTime(StartDateTime);
            model.EndDateTime = Utilities.ConvertStringToDateTime(EndDateTime);
            _repos.CreateEvent(model.Title,
                                model.Note,
                                model.StartDateTime,
                                model.EndDateTime,
                                model.IsAllDay,
                                GetRecuringType(model.recurType));
            return Redirect("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult RemoveAll()
        {
            _repos.DeleteAll();
            return Redirect("Index");
        }

        private RecuringType GetRecuringType(int type)
        {
            switch (type)
            {
                case 1:
                    return RecuringType.Day;
                case 2:
                    return RecuringType.Week;
                case 3:
                    return RecuringType.Month;
                case 4:
                    return RecuringType.Year;
                default:
                    return RecuringType.None;

            }
        }

        
    }
}
