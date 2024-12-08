﻿using CombatGameSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            // Get the user object for the currently logged in user
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

            model.Characters = _context.Characters
                .Where(c => c.UserId == model.SelectedUser.Id)
                .OrderBy(c => c.Name)
                .ToList();

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
        public ActionResult Edit()
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
        public ActionResult Edit(UserEditViewModel model)
        {
            model.CurrentUser = GetCurrentUser();

            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Account" });
            }

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
