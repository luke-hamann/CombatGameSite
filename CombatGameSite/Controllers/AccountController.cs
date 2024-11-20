using System.Diagnostics;
using CombatGameSite.Models;
using Microsoft.AspNetCore.Mvc;


namespace CombatGameSite.Controllers
{
    [Area("Account")]
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string Username, string password)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string Username, string Password, string TagLine)
        {
            return View();
        }
        
    }
}
