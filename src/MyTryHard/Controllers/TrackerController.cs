using Microsoft.AspNetCore.Mvc;
using MyTryHard.Models;

namespace MyTryHard.Controllers
{
    public class TrackerController : Controller
    {
        private ApplicationDbContext _ctx;

        public TrackerController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult SaveEntry()
        {
            return View();
        } 

        public IActionResult Entries()
        {
            return RedirectToAction("Index");
        }

        public IActionResult GetGraph()
        {
            return RedirectToAction("Index");
        }
    }
}
