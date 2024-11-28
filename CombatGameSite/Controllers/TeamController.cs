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
        private User? GetCurrentUser()
        {
            return _context.Users.Find(HttpContext.Session.GetInt32("userId"));
        }

        [NonAction]
        private void ValidateTeamEditViewModel(TeamEditViewModel model)
        {
            if (model.Team.CombatantIds.Count() == 0)
            {
                ModelState.AddModelError("Combatant1Id", "A team must have at least 1 combatant.");
            }
            else if (model.Team.CombatantIds.Distinct().Count() != model.Team.CombatantIds.Count())
            {
                ModelState.AddModelError("Combatant1Id", "A combatant can only appear once.");
            }

            foreach (int id in model.Team.CombatantIds)
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
        }

        [HttpGet]
        [Route("/team/add/")]
        public IActionResult Add()
        {
            var model = new TeamEditViewModel()
            {
                CurrentUser = GetCurrentUser(),
                Mode = "Add",
                Team = new Team()
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

            ValidateTeamEditViewModel(model);

            if (!ModelState.IsValid)
            {
                model.Combatants = _context.Combatants
                    .Where(c => c.UserId == model.CurrentUser.Id)
                    .OrderBy(c => c.Name)
                    .ToList();
                return View("Edit", model);
            }

            _context.Add(model.Team!);
            _context.SaveChanges();

            return RedirectToAction("Index", "User", new { id = model.CurrentUser.Id, section = "teams" });
        }

        [HttpGet]
        [Route("/team/{id}/edit/")]
        public IActionResult Edit(int id)
        {
            var model = new TeamEditViewModel()
            {
                CurrentUser = GetCurrentUser(),
                Mode = "Edit"
            };

            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }

            model.Team = _context.Teams
                .Where(t => t.Id == id && t.UserId == model.CurrentUser.Id)
                .FirstOrDefault();

            if (model.Team == null)
            {
                return NotFound();
            }

            model.Combatants = _context.Combatants
                .Where(c => c.UserId == model.CurrentUser.Id)
                .OrderBy(c => c.Name)
                .ToList();

            return View("Edit", model);
        }

        [HttpPost]
        [Route("/team/{id}/edit/")]
        public IActionResult Edit(int id, TeamEditViewModel model)
        {
            model.CurrentUser = GetCurrentUser();

            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }

            var team = _context.Teams.Find(id);
            _context.ChangeTracker.Clear();

            if (team == null)
            {
                return NotFound();
            }

            model.Team.Id = team.Id;
            model.Team.UserId = team.UserId;
            model.Team.Score = team.Score;

            ValidateTeamEditViewModel(model);

            if (!ModelState.IsValid)
            {
                model.Mode = "Edit";
                model.Combatants = _context.Combatants
                    .Where(c => c.UserId == model.CurrentUser.Id)
                    .OrderBy(c => c.Name)
                    .ToList();
                return View("Edit", model);
            }

            _context.Update(model.Team);
            _context.SaveChanges();

            return RedirectToAction("Index", "User", new { id = model.Team.UserId, section = "teams" });
        }

        [HttpGet]
        [Route("/team/{id}/delete/")]
        public IActionResult Delete(int id)
        {
            var model = new TeamDeleteViewModel()
            {
                CurrentUser = GetCurrentUser()
            };

            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }

            model.Team = _context.Teams
                .Where(t => t.Id == id && t.UserId == model.CurrentUser.Id)
                .FirstOrDefault();

            if (model.Team == null)
            {
                return NotFound();
            }

            return View("Delete", model);
        }

        [HttpPost]
        [Route("/team/{id}/delete/")]
        public IActionResult Delete(int id, TeamEditViewModel model)
        {
            model.CurrentUser = GetCurrentUser();

            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }

            var team = _context.Teams
                .Where(t => t.Id == id && t.UserId == model.CurrentUser.Id)
                .FirstOrDefault();
            _context.ChangeTracker.Clear();

            if (team == null)
            {
                return NotFound();
            }

            _context.Remove(team);
            _context.SaveChanges();

            return RedirectToAction("Teams", "User", new { id = team.UserId });
        }
    }
}
