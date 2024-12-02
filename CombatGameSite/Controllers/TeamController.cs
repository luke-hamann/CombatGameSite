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
            var team = _context.Teams
                .Where(t => t.Id != model.Team.Id && t.Name == model.Team.Name)
                .FirstOrDefault();
            if (team != null)
            {
                ModelState.AddModelError("Name", "You already have a team with that name.");
            }

            if (model.Team.CharacterIds.Count() == 0)
            {
                ModelState.AddModelError("Character1Id", "A team must have at least 1 character.");
            }
            else if (model.Team.CharacterIds.Distinct().Count() != model.Team.CharacterIds.Count())
            {
                ModelState.AddModelError("Character1Id", "A character can only appear once.");
            }

            foreach (int id in model.Team.CharacterIds)
            {
                var character = _context.Characters
                    .Where(c => c.Id == id && c.UserId == model.CurrentUser.Id)
                    .FirstOrDefault();
                if (character == null)
                {
                    ModelState.AddModelError("Character1Id", "An unavailable character is included.");
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

            model.Characters = _context.Characters
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

            // Show the add form again if there were validation errors
            if (!ModelState.IsValid)
            {
                model.Characters = _context.Characters
                    .Where(c => c.UserId == model.CurrentUser.Id)
                    .OrderBy(c => c.Name)
                    .ToList();
                return View("Edit", model);
            }

            model.Team!.UserId = model.CurrentUser.Id;
            model.Team.Score = 0;

            _context.Add(model.Team);
            _context.SaveChanges();

            TempData["message"] = $"You just added the team {model.Team.Name}.";

            return RedirectToAction("Teams", "User", new { id = model.CurrentUser.Id });
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

            model.Characters = _context.Characters
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
                model.Characters = _context.Characters
                    .Where(c => c.UserId == model.CurrentUser.Id)
                    .OrderBy(c => c.Name)
                    .ToList();
                return View("Edit", model);
            }

            _context.Update(model.Team);
            _context.SaveChanges();

            TempData["message"] = $"You just edited the team {model.Team.Name}.";

            return RedirectToAction("Teams", "User", new { id = model.Team.UserId });
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

            TempData["message"] = $"You just deleted the team {team.Name}.";

            return RedirectToAction("Teams", "User", new { id = team.UserId });
        }
    }
}
