using CombatGameSite.Areas.Account.Models;
using CombatGameSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CombatGameSite.Areas.Account.Controllers
{
    [Area("Account")]
    public class AccountController : Controller
    {
        private readonly CombatContext _context;

        public AccountController(CombatContext context)
        {
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
        public IActionResult Login()
        {
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
        public IActionResult Login(LoginViewModel model)
        {
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
        public IActionResult Register()
        {
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
        public IActionResult Register(RegisterViewModel model)
        {
            model.CurrentUser = GetCurrentUser();

            if (model.CurrentUser != null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Verify the user has a unique name
            if (model.Username != null)
            {
                User? conflictingUser = _context.Users
                    .Where(u => u.Name == model.Username)
                    .FirstOrDefault();

                if (conflictingUser != null)
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
        {
            HttpContext.Session.SetInt32("userId", 0);
            return RedirectToAction("Index", "Home");
        }
    }
}
