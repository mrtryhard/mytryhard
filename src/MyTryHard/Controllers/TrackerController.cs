using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTryHard.Models;
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
        public IActionResult SaveEntry()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [Route("tracker/sport/{sportId:int?}")]
        public IActionResult Entries(int sportId = -1)
        {
            if (sportId == -1)
                return RedirectToAction("Index");

            TrackerViewModel tvm = new TrackerViewModel();
            var userId = _userManager.GetUserId(HttpContext.User);
            tvm.Entries = _ctx.Tracker.GetEntriesForUserAndSport(Guid.Parse(userId), sportId);
            tvm.SportsList = _ctx.Tracker.GetSportsList();
            return View("Index", tvm);
        }

        public IActionResult GetGraph()
        {
            return RedirectToAction("Index");
        }
    }
}
