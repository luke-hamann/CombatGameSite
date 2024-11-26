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

            var model = new UserViewModel()
            {
                CurrentUser = GetCurrentUser(),
                SelectedUser = _context.Users.Find(id),
                SelectedSection = section
            };

            if (model.SelectedUser == null)
            {
                return NotFound();
            }

            if (model.SelectedSection == "combatants")
            {
                model.Combatants = _context.Combatants
                    .Where(c => c.UserId == id)
                    .OrderBy(c => c.Name)
                    .ToList();
            }
            else
            {
                model.Teams = _context.Teams
                    .Where(t => t.UserId == id)
                    .Include(t => t.Combatant1)
                    .Include(t => t.Combatant2)
                    .Include(t => t.Combatant3)
                    .Include(t => t.Combatant4)
                    .Include(t => t.Combatant5)
                    .OrderBy(t => t.Name)
                    .ToList();
            }

            return View(model);
        }
    }
}
