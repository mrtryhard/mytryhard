using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTryHard.Models;
using MyTryHard.Models.Tracker;
using MyTryHard.ViewModels.Tracker;
using System;

namespace MyTryHard.Controllers
{
    [Authorize]
    public class TrackerController : Controller
    {
        private ApplicationDbContext _ctx;
        private readonly UserManager<ApplicationUser> _userManager;

        public TrackerController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext ctx)
        {
            _userManager = userManager;
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            TrackerViewModel tvm = new TrackerViewModel();
            var userId = _userManager.GetUserId(HttpContext.User);
            tvm.Entries = _ctx.Tracker.GetEntriesForUser(Guid.Parse(userId));
            tvm.SportsList = _ctx.Tracker.GetSportsList();
            return View(tvm);
        }

        [HttpPost]
        public IActionResult Save()
        {
            DateTime dtStart = new DateTime(int.Parse(Request.Form["startYear"]), 
                int.Parse(Request.Form["startMonth"]),
                int.Parse(Request.Form["startDay"]), 
                int.Parse(Request.Form["startHour"]), 
                int.Parse(Request.Form["startMinute"]), 
                int.Parse(Request.Form["startSecond"]),
                int.Parse(Request.Form["startMillis"]));

            DateTime dtEnd = new DateTime(int.Parse(Request.Form["endYear"]),
                int.Parse(Request.Form["endMonth"]),
                int.Parse(Request.Form["endDay"]),
                int.Parse(Request.Form["endHour"]),
                int.Parse(Request.Form["endMinute"]),
                int.Parse(Request.Form["endSecond"]),
                int.Parse(Request.Form["endMillis"]));
            int distance = int.Parse(Request.Form["distance"]);
            int sportId = int.Parse(Request.Form["SportId"]);
            var userId = _userManager.GetUserId(HttpContext.User);

            _ctx.Tracker.SaveEntryForUser(Guid.Parse(userId), dtStart, dtEnd, distance, sportId);

            return RedirectToAction("index");
        }

        public IActionResult Add()
        {
            ViewBag.SportsList = _ctx.Tracker.GetSportsList();
            return View();
        }

        [Route("tracker/sport/{sportId:int?}")]
        public IActionResult Entries(int sportId = -1)
        {
            if (sportId == -1)
                return RedirectToAction("index");

            TrackerViewModel tvm = new TrackerViewModel();
            var userId = _userManager.GetUserId(HttpContext.User);
            tvm.Entries = _ctx.Tracker.GetEntriesForUserAndSport(Guid.Parse(userId), sportId);
            tvm.SportsList = _ctx.Tracker.GetSportsList();
            ViewBag.SportId = sportId;
            return View("Index", tvm);
        }

        public IActionResult EntriesRedirect(int sportId)
        {
            return RedirectToAction("entries", new { sportId = sportId });
        }

        public IActionResult GetGraph()
        {
            return RedirectToAction("Index");
        }
    }
}
