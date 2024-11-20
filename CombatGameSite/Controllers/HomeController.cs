using CombatGameSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CombatGameSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly CombatContext _context;

        public HomeController(CombatContext context)
        {
            _context = context;
        }

        [NonAction]
        private User? GetCurrentUser()
        {
            return _context.Users.Find(HttpContext.Session.GetInt32("userId"));
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return View(new { CurrentUser = GetCurrentUser() });
        }

        [HttpGet]
        public IActionResult Leaderboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Battle()
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Battle(int teamID)
        { //Pass in winning teams ID and add to their score. +10 for win -3 for loss
            return View();
        }
    }
}
