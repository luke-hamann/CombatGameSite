using System.Diagnostics;
using CombatGameSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CombatGameSite.Controllers
{
    [Area("Default")]
    public class UserController : Controller
    {
        
        [HttpGet]
        public IActionResult Character()
        {

            return View();
        }
        [HttpGet]
        public IActionResult Team()
        {
            return View();
        }
    }
}
