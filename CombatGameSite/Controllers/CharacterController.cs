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
            // Get the user object for the currenly logged in user
            return _context.Users.Find(HttpContext.Session.GetInt32("userId"));
        }

        [NonAction]
        private Character PopulateCharacterWithSkills(Character character)
        {
            // Populate a character object with skill objects based on the skill ids

            if (character.SkillPrimaryId != null)
            {
                character.SkillPrimary = _context.Skills.Find(character.SkillPrimaryId);
            }

            if (character.SkillSecondaryId != null)
            {
                character.SkillSecondary = _context.Skills.Find(character.SkillSecondaryId);
            }

            if (character.SkillTertiaryId != null)
            {
                character.SkillTertiary = _context.Skills.Find(character.SkillTertiaryId);
            }

            return character;
        }

        [HttpGet]
        [Route("/character/{id}/")]
        public IActionResult Index(int id)
        {
            // Ensure the character exists
            Character? character = _context.Characters
                .Include(c => c.User)
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (character == null)
            {
                return NotFound();
            }

            // Prepare the view model
            character = PopulateCharacterWithSkills(character);
            var model = new CharacterIndexViewModel
            {
                CurrentUser = GetCurrentUser(),
                Character = character
            };

            return View(model);
        }

        [HttpGet]
        [Route("/character/add/")]
        public IActionResult Add() 
        {
            // Ensure the user is logged in
            var model = new CharacterEditViewModel()
            {
                CurrentUser = GetCurrentUser()
            };

            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }

            // Show the add character form
            model.Mode = "Add";
            model.Character = new Character();
            model.Character.Health = 100;
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
            model.Character.UserId = model.CurrentUser.Id;

            // Ensure the character name is unique
            var character = _context.Characters
                .Where(c => c.UserId == model.CurrentUser.Id && c.Name == model.Character.Name)
                .FirstOrDefault();
            _context.ChangeTracker.Clear();
            if (character != null)
            {
                ModelState.AddModelError("Character.Name", "You already have a character with that name.");
            }

            // Set the character's skill ids based on the skill id list
            model.SetSkills();

            // Ensure the skill point distribution is valid
            model.Character = PopulateCharacterWithSkills(model.Character);
            if (!model.Character.hasValidSkillPointDistribution())
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

            // Add the character
            _context.Add(model.Character);
            _context.SaveChanges();

            // Return to the user's character list
            TempData["message"] = $"You just created the character {model.Character.Name}.";
            return RedirectToAction("Index", "Character", new { id = model.Character.Id });
        }

        [HttpGet]
        [Route("/character/{id}/edit/")]
        public IActionResult Edit(int id)
        {
            // Ensure the user is logged in
            var model = new CharacterEditViewModel()
            {
                CurrentUser = GetCurrentUser()
            };

            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }

            // Ensure the character exists and is owned by the logged in user
            model.Character = _context.Characters
                .Where(c => c.Id == id && c.UserId == model.CurrentUser.Id)
                .FirstOrDefault();

            if (model.Character == null)
            {
                return NotFound();
            }

            // Show the character edit form
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

            // Ensure the character exists and is owned by the logged in user
            var character = _context.Characters
                .Where(c => c.Id == id && c.UserId == model.CurrentUser.Id)
                .FirstOrDefault();
            if (character == null)
            {
                return NotFound();
            }
            _context.ChangeTracker.Clear();

            // Update the model with the necessary ids
            model.Character.Id = id;
            model.Character.UserId = model.CurrentUser.Id;

            // Ensure the character name is unique
            character = _context.Characters
                .Where(c => c.Name == model.Character.Name && c.Id != id)
                .FirstOrDefault();
            if (character != null)
            {
                ModelState.AddModelError("Character.Name", "You already have a character with that name.");
            }
            _context.ChangeTracker.Clear();

            // Set the character's skill ids based on the skill id list
            model.SetSkills();

            // Ensure the skill point distribution is valid
            model.Character = PopulateCharacterWithSkills(model.Character);
            if (!model.Character.hasValidSkillPointDistribution())
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

            // Update and save the character
            _context.Update(model.Character);
            _context.SaveChanges();

            // Return to the user's character list page
            TempData["message"] = $"You just edited the character {model.Character.Name}.";
            return RedirectToAction("Index", "Character", new { id = model.Character.Id });
        }

        [HttpGet]
        [Route("/character/{id}/delete/")]
        public IActionResult Delete(int id)
        {
            // Ensure the user is logged in
            var model = new CharacterDeleteViewModel()
            {
                CurrentUser = GetCurrentUser()
            };

            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }

            // Verify the character exists and is owned by the logged in user
            model.Character = _context.Characters
                .Where(c => c.Id == id && c.UserId == model.CurrentUser.Id)
                .FirstOrDefault();
            _context.ChangeTracker.Clear();

            if (model.Character == null)
            {
                return NotFound();
            }

            // Get all the teams the character is a part of
            model.Teams = _context.Teams
                .Where(t => t.UserId == model.CurrentUser.Id)
                .Where(t => t.Character1Id == id || t.Character2Id == id || t.Character3Id == id ||
                            t.Character4Id == id || t.Character5Id == id)
                .OrderBy(t => t.Name)
                .ToList();

            return View("Delete", model);
        }

        [HttpPost]
        [Route("/character/{id}/delete/")]
        public IActionResult Delete(int id, CharacterDeleteViewModel model)
        {
            // Verify the user is logged in
            model.CurrentUser = GetCurrentUser();
            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }

            // Ensure the character exists and is owned by the logged in user
            var character = _context.Characters
                .Where(c => c.Id == id && c.UserId == model.CurrentUser.Id)
                .FirstOrDefault();

            if (character == null)
            {
                return NotFound();
            }

            // Remove the character from teams they are a part of and
            // delete teams that only consist of the character

            var teams = _context.Teams
                .Where(t => t.Character1Id == character.Id || t.Character2Id == character.Id || t.Character3Id == character.Id ||
                            t.Character4Id == character.Id || t.Character5Id == character.Id)
                .ToList();

            foreach (Team team in teams)
            {
                if (team.CharacterIds.Count == 1)
                {
                    _context.Remove(team);
                }
                else
                {
                    if (team.Character1Id == id) team.Character1Id = null;
                    if (team.Character2Id == id) team.Character2Id = null;
                    if (team.Character3Id == id) team.Character3Id = null;
                    if (team.Character4Id == id) team.Character4Id = null;
                    if (team.Character5Id == id) team.Character5Id = null;
                }
            }

            // Remove the character
            _context.Remove(character);
            _context.SaveChanges();

            // Return to the user's character list page
            TempData["message"] = $"You just deleted the character {character.Name}.";
            return RedirectToAction("Characters", "User", new { id = character.UserId });
        }
    }
}
