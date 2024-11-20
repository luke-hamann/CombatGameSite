using System.Diagnostics;
using CombatGameSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CombatGameSite.Controllers
{
    [Area("Default")]
    public class HomeController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
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
