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
        {
            _context = context;
        }

        [NonAction]
        public User? GetCurrentUser()
        {
            return _context.Users.Find(HttpContext.Session.GetInt32("userId"));
        }

        [HttpGet]
        [Route("/user/{id}/")]
        public IActionResult Index(int id)
        {
            return Characters(id);
        }

        [HttpGet]
        [Route("/user/{id}/characters/")]
        public IActionResult Characters(int id)
        {
            var model = new UserViewModel()
            {
                CurrentUser = GetCurrentUser(),
                SelectedUser = _context.Users.Find(id)
            };

            if (model.SelectedUser ==  null)
            {
                return NotFound();
            }

            model.Characters = _context.Characters
                .Where(c => c.UserId == model.SelectedUser.Id)
                .OrderBy(c => c.Name)
                .ToList();

            return View("Characters", model);
        }

        [HttpGet]
        [Route("/user/{id}/teams/")]
        public IActionResult Teams(int id)
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

            model.Teams = _context.Teams
                .Where(t => t.UserId == id)
                .Include(t => t.Character1)
                .Include(t => t.Character2)
                .Include(t => t.Character3)
                .Include(t => t.Character4)
                .Include(t => t.Character5)
                .OrderBy(t => t.Name)
                .ToList();

            return View("Teams", model);
        }

        [HttpGet]
        [Route("/user/edit/")]
        public IActionResult Edit()
        {
            var model = new UserEditViewModel()
            {
                CurrentUser = GetCurrentUser()
            };

            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }

            model.User = model.CurrentUser;

            return View(model);
        }

        [HttpPost]
        [Route("/user/edit/")]
        public IActionResult Edit(UserEditViewModel model)
        {
            model.CurrentUser = GetCurrentUser();

            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }

            model.CurrentUser.Tagline = model.User.Tagline;

            _context.Update(model.CurrentUser);
            _context.SaveChanges();

            return RedirectToAction("Index", "User", new { id = model.CurrentUser.Id });
        }
    }
}
