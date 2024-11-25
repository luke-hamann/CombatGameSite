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

        [HttpGet]
        [Route("/character/{id}/")]
        public IActionResult Index(int id)
        {
            Combatant? combatant = _context.Combatants
                .Include(c => c.User)
                .Include(c => c.SkillPrimary)
                .Include(c => c.SkillSecondary)
                .Include(c => c.SkillTertiary)
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (combatant == null)
            {
                return NotFound();
            }

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
            var model = new CharacterEditViewModel();

            model.CurrentUser = GetCurrentUser();

            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Account" });
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
            model.Mode = "Add";
            model.CurrentUser = GetCurrentUser();
            model.Skills = _context.Skills
                .OrderBy(s => s.Name)
                .ToList();

            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Account" });
            }

            model.Combatant.UserId = model.CurrentUser.Id;

            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            _context.Add(model.Combatant);
            _context.SaveChanges();

            return RedirectToAction("Index", new { id = model.Combatant.Id });
        }

        [HttpGet]
        public IActionResult Delete()
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int character)
        { //Change int to character class once built
            return View();
        }

    }
}
