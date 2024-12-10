using CombatGameSite.Areas.Account.Models;
using CombatGameSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CombatGameSite.Areas.Account.Controllers
{ //Controller to handle account data
    [Area("Account")]
    public class AccountController : Controller
    {
        private readonly CombatContext _context;

        public AccountController(CombatContext context)
        { //Constructor method for establishing context
            _context = context;
        }

        [NonAction]
        private User? GetCurrentUser()
        {
            // Get the user object for the currently logged in user
            return _context.Users.Find(HttpContext.Session.GetInt32("userId"));
        }

        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        { //Check that a user is logged in. Redirect to home page if there is a user logged in.
            var model = new LoginViewModel()
            {
                CurrentUser = GetCurrentUser()
            };

            if (model.CurrentUser != null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View(model);
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(LoginViewModel model)
        { //Log a user in with their password and save their id in session data. Redirect to Index of Home controller if there is already a user logged in.
            model.CurrentUser = GetCurrentUser();

            if (model.CurrentUser != null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Validate the user's credentials
            User? user = _context.Users
                .Where(u => u.Name == model.Name && u.Password == model.Password)
                .FirstOrDefault();

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid credentials.");
                return View(model);
            }

            // Update the session
            HttpContext.Session.SetInt32("userId", user.Id);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("register")]
        public ActionResult Register()
        {//Display the register form. Redirect to Index of Home controller if there is already a user logged in.
            var model = new RegisterViewModel()
            {
                CurrentUser = GetCurrentUser()
            };

            if (model.CurrentUser != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpPost]
        [Route("register")]
        public ActionResult Register(RegisterViewModel model)
        {//Create a new user in the database. 
            model.CurrentUser = GetCurrentUser();

            if (model.CurrentUser != null) //Redirect to Index action of Home controller if user is logged in
            {
                return RedirectToAction("Index", "Home");
            }

            // Verify the user has a unique name
            if (model.Username != null)
            {
                User? conflictingUser = _context.Users
                    .Where(u => u.Name == model.Username)
                    .FirstOrDefault();

                if (conflictingUser != null) //Raise an error if the username is already taken
                {
                    ModelState.AddModelError("", "Username is taken.");
                }
            }

            // Verify the passwords match
            if (model.Password != model.PasswordConfirm)
            {
                ModelState.AddModelError("", "Passwords do not match.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = model.ToUser();
            _context.Add(user);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("userId", user.Id);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Route("logout")]
        public RedirectToActionResult Logout()
        { //Log user out by removing their id from session data. Redirect to Index action of Home controller.
            HttpContext.Session.SetInt32("userId", 0);
            return RedirectToAction("Index", "Home");
        }
    }
}
