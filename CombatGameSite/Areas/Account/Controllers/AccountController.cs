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
            return _context.Users.Find(HttpContext.Session.GetInt32("userId"));
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            var loginViewModel = new LoginViewModel();
            loginViewModel.CurrentUser = GetCurrentUser();

            if (loginViewModel.CurrentUser != null)
            {
                return Redirect("/");
            }
            
            return View(loginViewModel);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            loginViewModel.CurrentUser = GetCurrentUser();

            if (loginViewModel.CurrentUser != null)
            {
                return Redirect("/");
            }

            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            User? user = _context.Users
                .Where(u => u.Name == loginViewModel.Name && u.Password == loginViewModel.Password)
                .FirstOrDefault();

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid credentials.");
                return View(loginViewModel);
            }

            HttpContext.Session.SetInt32("userId", user.Id);
            return Redirect("/");
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            var registerViewModel = new RegisterViewModel();
            registerViewModel.CurrentUser = GetCurrentUser();

            if (registerViewModel.CurrentUser != null)
            {
                return Redirect("/");
            }

            return View(registerViewModel);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            registerViewModel.CurrentUser = GetCurrentUser();

            if (registerViewModel.CurrentUser != null)
            {
                return Redirect("/");
            }

            if (registerViewModel.Username != null)
            {
                User? conflictingUser = _context.Users
                    .Where(u => u.Name == registerViewModel.Username)
                    .FirstOrDefault();
                if (conflictingUser != null)
                {
                    ModelState.AddModelError("", "Username is taken.");
                }
            }

            if (registerViewModel.Password != registerViewModel.PasswordConfirm)
            {
                ModelState.AddModelError("", "Passwords do not match.");
            }

            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var user = registerViewModel.toUser();
            _context.Add(user);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("userId", user.Id);
            return Redirect("/");
        }

        [HttpPost]
        [Route("logout")]
        public RedirectResult Logout(string returnTo = "/")
        {
            HttpContext.Session.SetInt32("userId", 0);
            return Redirect(returnTo);
        }
    }
}
