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
        [Route("/user/{id}/{section?}")]
        public IActionResult Index(int id, string section = "combatants")
        {
            if (section != "combatants" && section != "teams")
            {
                return NotFound();
            }

            User? selectedUser = _context.Users.Find(id);

            if (selectedUser == null)
            {
                return NotFound();
            }

            List<Combatant> combatants = [];
            List<Team> teams = [];
            if (section == "combatants")
            {
                combatants = _context.Combatants
                    .Where(c => c.UserId == id)
                    .OrderBy(c => c.Name)
                    .ToList();
            }
            else
            {
                teams = _context.Teams
                    .Where(t => t.UserId == id)
                    .Include(t => t.Combatant1)
                    .Include(t => t.Combatant2)
                    .Include(t => t.Combatant3)
                    .Include(t => t.Combatant4)
                    .Include(t => t.Combatant5)
                    .OrderBy(t => t.Name)
                    .ToList();
            }

            var model = new UserViewModel
            {
                CurrentUser = GetCurrentUser(),
                SelectedUser = selectedUser,
                SelectedSection = section,
                Combatants = combatants,
                Teams = teams
            };

            return View(model);
        }
    }
}
