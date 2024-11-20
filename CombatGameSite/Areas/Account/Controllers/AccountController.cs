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
            if (ViewBag.currentUser != null)
            {
                return Redirect("/");
            }

            return View(new LoginViewModel());
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ViewBag.currentUser != null)
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

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout(string returnTo = "/")
        {
            HttpContext.Session.SetInt32("userId", 0);
            return Redirect(returnTo);
        }
    }
}
