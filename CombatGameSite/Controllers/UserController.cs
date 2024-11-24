using System.Diagnostics;
using CombatGameSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CombatGameSite.Controllers
{
    public class UserController : Controller
    {
        private CombatContext _context;

        public UserController(CombatContext context)
        {
            _context = context;
        }

        [NonAction]
        public User? GetCurrentUser()
        {
            return _context.Users.Find(HttpContext.Session.GetInt32("userId"));
        }

        [HttpGet]
        [Route("/user/{id}/characters/")]
        public IActionResult Characters(int id)
        {
            User? selectedUser = _context.Users.Find(id);

            if (selectedUser == null)
            {
                return NotFound();
            }

            List<Combatant> combatants = _context.Combatants
                .Where(c => c.UserId == id)
                .OrderBy(c => c.Name)
                .ToList();

            var model = new UserCombatantsViewModel
            {
                CurrentUser = GetCurrentUser(),
                SelectedUser = selectedUser,
                Combatants = combatants
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Teams()
        {
            return View();
        }
    }
}
