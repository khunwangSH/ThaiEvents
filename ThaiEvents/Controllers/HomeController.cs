using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThaiEvents.Data;
using ThaiEvents.Models;

namespace ThaiEvents.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repos;

        public HomeController(ILogger<HomeController> logger,
                              IRepository repos)
        {
            _logger = logger;
            _repos = repos;
        }

        public IActionResult Index()
        {
            _repos.CreateEvent("Title Test","Event Note",DateTime.UtcNow,DateTime.UtcNow.AddHours(2),false);
            _repos.CreateEvent("Title Test2", "Event Note2", DateTime.UtcNow.AddMonths(2), DateTime.UtcNow.AddMonths(2).AddHours(2), false);
            return View(_repos.GetEvents());
        }

        [HttpPost]
        public IActionResult Index(string search)
        {
            var s = search.ToLower();
            return View(_repos.GetEvents().Where(w => w.Title.ToLower().Contains(s) || w.Note.ToLower().Contains(s)).ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
