using CombatGameSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CombatGameSite.Controllers
{
    public class TeamController : Controller
    {
        private CombatContext _context;
        public TeamController(CombatContext context)
        {
            _context = context;
        }

        [NonAction]
        public User? GetCurrentUser()
        {
            return _context.Users.Find(HttpContext.Session.GetInt32("userId"));
        }

        [HttpGet]
        [Route("/team/add/")]
        public IActionResult Add()
        {
            var model = new TeamEditViewModel()
            {
                CurrentUser = GetCurrentUser(),
                Mode = "Add"
            };

            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }

            model.Combatants = _context.Combatants
                .Where(c => c.UserId == model.CurrentUser.Id)
                .OrderBy(c => c.Name)
                .ToList();

            return View("Edit", model);
        }

        [HttpPost]
        [Route("/team/add/")]
        public IActionResult Add(TeamEditViewModel model)
        {
            model.Mode = "Add";
            model.CurrentUser = GetCurrentUser();

            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }

            var combatantIds = new List<int?>
            {
                model.Combatant1Id, model.Combatant2Id, model.Combatant3Id,
                model.Combatant4Id, model.Combatant5Id
            };

            combatantIds = combatantIds.Where(i => i != null).ToList();

            if (combatantIds.Count() == 0)
            {
                ModelState.AddModelError("Combatant1Id", "A team must have at least 1 combatant.");
            }
            else if (combatantIds.Distinct().Count() != combatantIds.Count())
            {
                ModelState.AddModelError("Combatant1Id", "A combatant can only appear once.");
            }

            foreach (int id in combatantIds)
            {
                var combatant = _context.Combatants
                    .Where(c => c.Id == id && c.UserId == model.CurrentUser.Id)
                    .FirstOrDefault();
                if (combatant == null)
                {
                    ModelState.AddModelError("Combatant1Id", "An unavailable combatant is included.");
                    break;
                }
            }

            if (!ModelState.IsValid)
            {
                model.Combatants = _context.Combatants
                    .Where(c => c.UserId == model.CurrentUser.Id)
                    .OrderBy(c => c.Name)
                    .ToList();
                return View("Edit", model);
            }

            _context.Add(model.GetTeam());
            _context.SaveChanges();

            return RedirectToAction("Index", "User", new { id = model.CurrentUser.Id, section = "teams" });
        }

        [HttpGet]
        [Route("/team/{id}/edit/")]
        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [Route("/team/{id}/edit/")]
        public IActionResult Edit(int id, TeamEditViewModel model)
        {
            return View();
        }

        [HttpGet]
        [Route("/team/{id}/delete/")]
        public IActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [Route("/team/{id}/delete/")]
        public IActionResult Delete(int id, TeamEditViewModel model)
        {
            return View();
        }
    }
}
