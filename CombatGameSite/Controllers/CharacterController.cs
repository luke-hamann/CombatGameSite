using CombatGameSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CombatGameSite.Controllers
{
    public class CharacterController : Controller
    {
        private CombatContext _context;

        public CharacterController(CombatContext context)
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
        [Route("/character/{id}/")]
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

            var model = new CharacterIndexViewModel
            {
                CurrentUser = GetCurrentUser(),
                Combatant = combatant
            };

            return View(model);
        }

        [HttpGet]
        [Route("/character/add/")]
        public IActionResult Add() 
        {
            var model = new CharacterEditViewModel()
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
        [Route("/character/add/")]
        public IActionResult Add(CharacterEditViewModel model)
        {
            // Ensure the user is logged in
            model.CurrentUser = GetCurrentUser();
            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }
            model.Combatant.UserId = model.CurrentUser.Id;

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
        [Route("/character/{id}/edit/")]
        public IActionResult Edit(int id)
        {
            var model = new CharacterEditViewModel()
            {
                CurrentUser = GetCurrentUser()
            };

            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }

            model.Combatant = _context.Combatants.Find(id);

            if (model.Combatant == null || model.Combatant.UserId != model.CurrentUser.Id)
            {
                return NotFound();
            }

            model.Mode = "Edit";
            model.Skills = _context.Skills.OrderBy(s => s.Name).ToList();
            return View("Edit", model);
        }

        [HttpPost]
        [Route("/character/{id}/edit/")]
        public IActionResult Edit(CharacterEditViewModel model, int id)
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
            _context.ChangeTracker.Clear(); // EF Core can't update the combatant without this

            model.Combatant.Id = id;
            model.Combatant.UserId = model.CurrentUser.Id;

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

            _context.Update(model.Combatant);
            _context.SaveChanges();
            return RedirectToAction("Index", new { id = model.Combatant.Id });
        }

        [HttpGet]
        [Route("/character/{id}/delete/")]
        public IActionResult Delete()
        { 
            return View();
        }

        [HttpPost]
        [Route("/character/{id}/delete/")]
        public IActionResult Delete(int character)
        { //Change int to character class once built
            return View();
        }

    }
}
