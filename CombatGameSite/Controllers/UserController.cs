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
        {//Constructor method. Stores context into private variable _context
            _context = context;
        }

        [NonAction]
        public User? GetCurrentUser()
        {//Method to get userId from Session data.
            return _context.Users.Find(HttpContext.Session.GetInt32("userId"));
        }

        [HttpGet]
        [Route("/user/{id}/")]
        public IActionResult Index(int id)
        {//Get character view with an ID from the route
            return Characters(id);
        }

        [HttpGet]
        [Route("/user/{id}/characters/")]
        public IActionResult Characters(int id)
        {//Get list of characters based on user ID
            var model = new UserViewModel()
            {
                CurrentUser = GetCurrentUser(),
                SelectedUser = _context.Users.Find(id)
            };

            if (model.SelectedUser ==  null)
            { //Return an error page if there is no selected user.
                return NotFound();
            }

            //Order .Characters and set them to a List
            model.Characters = _context.Characters
                .Where(c => c.UserId == model.SelectedUser.Id)
                .OrderBy(c => c.Name)
                .ToList();
            //Send model to Characters.cshtml
            return View("Characters", model);
        }

        [HttpGet]
        [Route("/user/{id}/teams/")]
        public IActionResult Teams(int id)
        {//View a list of teams using an id from the route.
            var model = new UserViewModel()
            {
                CurrentUser = GetCurrentUser(),
                SelectedUser = _context.Users.Find(id)
            };

            if (model.SelectedUser == null)
            { //If there is not a selected user, return an error page.
                return NotFound();
            }

            //Order the team of 5 by their name and store it as a List
            model.Teams = _context.Teams
                .Where(t => t.UserId == id)
                .Include(t => t.Character1)
                .Include(t => t.Character2)
                .Include(t => t.Character3)
                .Include(t => t.Character4)
                .Include(t => t.Character5)
                .OrderBy(t => t.Name)
                .ToList();

            //Give the Teams.cshtml the model
            return View("Teams", model);
        }

        [HttpGet]
        [Route("/user/edit/")]
        public IActionResult Edit()
        {//Display edit page
            var model = new UserEditViewModel()
            {//Set the user to be displayed as the user in Session Data
                CurrentUser = GetCurrentUser()
            };

            if (model.CurrentUser == null)
            {//If there is not a user logged in, redirect to Login page.
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }

            model.User = model.CurrentUser;
            //Go to Edit.cshtml and send it the model.
            return View(model);
        }

        [HttpPost]
        [Route("/user/edit/")]
        public IActionResult Edit(UserEditViewModel model)
        {// Update the database with the new User Tagline
            model.CurrentUser = GetCurrentUser();

            if (model.CurrentUser == null)
            {// Check if the current user is valid
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }

            model.CurrentUser.Tagline = model.User.Tagline;

            _context.Update(model.CurrentUser);
            _context.SaveChanges();

            return RedirectToAction("Index", "User", new { id = model.CurrentUser.Id });
        }
    }
}
