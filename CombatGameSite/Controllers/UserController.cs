using CombatGameSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;

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
        [Route("/user/{id}/")]
        public IActionResult Index(int id)
        {
            return Combatants(id);
        }

        [HttpGet]
        [Route("/user/{id}/combatants/")]
        public IActionResult Combatants(int id)
        {
            var model = new UserViewModel()
            {
                CurrentUser = GetCurrentUser(),
                SelectedUser = _context.Users.Find(id)
            };

            if (model.SelectedUser ==  null)
            {
                return NotFound();
            }

            model.Combatants = _context.Combatants
                .Where(c => c.UserId == model.SelectedUser.Id)
                .OrderBy(c => c.Name)
                .ToList();

            return View("Combatants", model);
        }

        [HttpGet]
        [Route("/user/{id}/teams/")]
        public IActionResult Teams(int id)
        {
            var model = new UserViewModel()
            {
                CurrentUser = GetCurrentUser(),
                SelectedUser = _context.Users.Find(id)
            };

            if (model.SelectedUser == null)
            {
                return NotFound();
            }

            model.Teams = _context.Teams
                .Where(t => t.UserId == id)
                .Include(t => t.Combatant1)
                .Include(t => t.Combatant2)
                .Include(t => t.Combatant3)
                .Include(t => t.Combatant4)
                .Include(t => t.Combatant5)
                .OrderBy(t => t.Name)
                .ToList();

            return View("Teams", model);
        }
    }
}
