using CombatGameSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

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
        public ActionResult Index(int id)
        {
            return Characters(id);
        }

        [HttpGet]
        [Route("/user/{id}/characters/")]
        public ActionResult Characters(int id)
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
        public ActionResult Teams(int id)
        {
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
        public ActionResult Edit()
        {
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
        public ActionResult Edit(UserEditViewModel model)
        {
            model.CurrentUser = GetCurrentUser();

            if (model.CurrentUser == null)
            {// Check if the current user is valid
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }

            // Ignore the username and password
            ModelState["User.Name"]!.ValidationState = ModelValidationState.Valid;
            ModelState["User.Password"]!.ValidationState = ModelValidationState.Valid;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Update the current user based on the form
            model.CurrentUser.Tagline = model.User!.Tagline;
            model.CurrentUser.FavoriteBook = model.User.FavoriteBook;
            model.CurrentUser.FavoriteGame = model.User.FavoriteGame;
            model.CurrentUser.FavoriteMovie = model.User.FavoriteMovie;

            _context.Update(model.CurrentUser);
            _context.SaveChanges();

            return RedirectToAction("Index", "User", new { id = model.CurrentUser.Id });
        }
    }
}
