using CombatGameSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CombatGameSite.Controllers
{
    public class CombatantController : Controller
    {
        private CombatContext _context;

        public CombatantController(CombatContext context)
        {
            _context = context;
        }

        [NonAction]
        private User? GetCurrentUser()
        {
            return _context.Users.Find(HttpContext.Session.GetInt32("userId"));
        }

        [NonAction]
        private Combatant PopulateCombatantWithSkills(Combatant combatant)
        {
            if (combatant.SkillPrimaryId != null)
            {
                combatant.SkillPrimary = _context.Skills.Find(combatant.SkillPrimaryId);
            }

            if (combatant.SkillSecondaryId != null)
            {
                combatant.SkillSecondary = _context.Skills.Find(combatant.SkillSecondaryId);
            }

            if (combatant.SkillTertiaryId != null)
            {
                combatant.SkillTertiary = _context.Skills.Find(combatant.SkillTertiaryId);
            }

            return combatant;
        }

        [HttpGet]
        [Route("/combatant/{id}/")]
        public IActionResult Index(int id)
        {
            Combatant? combatant = _context.Combatants
                .Include(c => c.User)
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (combatant == null)
            {
                return NotFound();
            }

            combatant = PopulateCombatantWithSkills(combatant);

            var model = new CombatantIndexViewModel
            {
                CurrentUser = GetCurrentUser(),
                Combatant = combatant
            };

            return View(model);
        }

        [HttpGet]
        [Route("/combatant/add/")]
        public IActionResult Add() 
        {
            var model = new CombatantEditViewModel()
            {
                CurrentUser = GetCurrentUser()
            };

            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }

            model.Mode = "Add";
            model.Skills = _context.Skills
                .OrderBy(s => s.Name)
                .ToList();

            return View("Edit", model);
        }

        [HttpPost]
        [Route("/combatant/add/")]
        public IActionResult Add(CombatantEditViewModel model)
        {
            // Ensure the user is logged in
            model.CurrentUser = GetCurrentUser();
            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }
            model.Combatant.UserId = model.CurrentUser.Id;

            // Ensure the combatant name is unique
            var combatant = _context.Combatants
                .Where(c => c.UserId == model.CurrentUser.Id && c.Name == model.Combatant.Name)
                .FirstOrDefault();
            _context.ChangeTracker.Clear();
            if (combatant != null)
            {
                ModelState.AddModelError("Combatant.Name", "You already have a combatant with that name.");
            }

            // Ensure the skill point distribution is valid
            model.Combatant = PopulateCombatantWithSkills(model.Combatant);
            if (!model.Combatant.hasValidSkillPointDistribution())
            {
                ModelState.AddModelError("", "Not enough skill points.");
            }

            // Show the form again if there were validation errors
            if (!ModelState.IsValid)
            {
                model.Mode = "Add";
                model.Skills = _context.Skills
                    .OrderBy(s => s.Name)
                    .ToList();
                return View("Edit", model);
            }

            _context.Add(model.Combatant);
            _context.SaveChanges();

            return RedirectToAction("Index", new { id = model.Combatant.Id });
        }

        [HttpGet]
        [Route("/combatant/{id}/edit/")]
        public IActionResult Edit(int id)
        {
            // Ensure the user is logged in
            var model = new CombatantEditViewModel()
            {
                CurrentUser = GetCurrentUser()
            };

            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }

            // Ensure the combatant exists and is owned by the logged in user
            model.Combatant = _context.Combatants.Find(id);
            if (model.Combatant == null || model.Combatant.UserId != model.CurrentUser.Id)
            {
                return NotFound();
            }

            // Show the combatant edit form
            model.Mode = "Edit";
            model.Skills = _context.Skills.OrderBy(s => s.Name).ToList();
            return View("Edit", model);
        }

        [HttpPost]
        [Route("/combatant/{id}/edit/")]
        public IActionResult Edit(CombatantEditViewModel model, int id)
        {
            // Ensure the user is logged in
            model.CurrentUser = GetCurrentUser();
            if (model.CurrentUser == null)
            {
                return NotFound();
            }

            // Ensure the combatant exists and is owned by the logged in user
            var combatant = _context.Combatants.Find(id);
            if (combatant == null || combatant.UserId != model.CurrentUser.Id)
            {
                return NotFound();
            }
            _context.ChangeTracker.Clear();

            // Update the model with the necessary ids
            model.Combatant.Id = id;
            model.Combatant.UserId = model.CurrentUser.Id;

            // Ensure the combatant name is unique
            combatant = _context.Combatants
                .Where(c => c.Name == model.Combatant.Name && c.Id != id)
                .FirstOrDefault();
            if (combatant != null)
            {
                ModelState.AddModelError("Combatant.Name", "You already have a combatant with that name.");
            }
            _context.ChangeTracker.Clear();

            // Ensure the skill point distribution is valid
            model.Combatant = PopulateCombatantWithSkills(model.Combatant);
            if (!model.Combatant.hasValidSkillPointDistribution())
            {
                ModelState.AddModelError("", "Not enough skill points.");
            }

            // Show the form again if there were validation errors
            if (!ModelState.IsValid)
            {
                model.Mode = "Edit";
                model.Skills = _context.Skills.OrderBy(s => s.Name).ToList();
                return View("Edit", model);
            }

            // Update and save the combatant
            _context.Update(model.Combatant);
            _context.SaveChanges();
            return RedirectToAction("Index", new { id = model.Combatant.Id });
        }

        [HttpGet]
        [Route("/combatant/{id}/delete/")]
        public IActionResult Delete(int id)
        {
            var model = new CombatantDeleteViewModel()
            {
                CurrentUser = GetCurrentUser()
            };

            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }

            model.Combatant = _context.Combatants
                .Where(c => c.Id == id && c.UserId == model.CurrentUser.Id)
                .FirstOrDefault();
            _context.ChangeTracker.Clear();

            if (model.Combatant == null)
            {
                return NotFound();
            }

            model.Teams = _context.Teams
                .Where(t => t.UserId == model.CurrentUser.Id)
                .Where(t => t.Combatant1Id == id || t.Combatant2Id == id || t.Combatant3Id == id ||
                            t.Combatant4Id == id || t.Combatant5Id == id)
                .OrderBy(t => t.Name)
                .ToList();

            return View("Delete", model);
        }

        [HttpPost]
        [Route("/combatant/{id}/delete/")]
        public IActionResult Delete(int id, CombatantDeleteViewModel model)
        {
            model.CurrentUser = GetCurrentUser();

            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }

            var combatant = _context.Combatants
                .Where(c => c.Id == id && c.UserId == model.CurrentUser.Id)
                .FirstOrDefault();

            if (combatant == null)
            {
                return NotFound();
            }

            // Remove the combatant from teams they are a part of and
            // delete teams that only consist of the combatant

            var teams = _context.Teams
                .Where(t => t.Combatant1Id == combatant.Id || t.Combatant2Id == combatant.Id || t.Combatant3Id == combatant.Id ||
                            t.Combatant4Id == combatant.Id || t.Combatant5Id == combatant.Id)
                .ToList();

            foreach (Team team in teams)
            {
                if (team.CombatantIds.Count == 1)
                {
                    _context.Remove(team);
                }
                else
                {
                    if (team.Combatant1Id == id) team.Combatant1Id = null;
                    if (team.Combatant2Id == id) team.Combatant2Id = null;
                    if (team.Combatant3Id == id) team.Combatant3Id = null;
                    if (team.Combatant4Id == id) team.Combatant4Id = null;
                    if (team.Combatant5Id == id) team.Combatant5Id = null;
                }
            }

            _context.Remove(combatant);
            _context.SaveChanges();

            return RedirectToAction("Combatants", "User", new { id = combatant.UserId });
        }
    }
}
