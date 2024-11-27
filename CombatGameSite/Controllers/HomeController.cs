using CombatGameSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CombatGameSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly CombatContext _context;

        public HomeController(CombatContext context)
        {
            _context = context;
        }

        [NonAction]
        private User? GetCurrentUser()
        {
            return _context.Users.Find(HttpContext.Session.GetInt32("userId"));
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return View(new { CurrentUser = GetCurrentUser() });
        }

        [HttpGet]
        public IActionResult Leaderboard()
        {
            var model = new LeaderboardViewModel()
            {
                CurrentUser = GetCurrentUser(),
                Teams = _context.Teams
                    .Include(t => t.User)
                    .Include(t => t.Combatant1)
                    .Include(t => t.Combatant2)
                    .Include(t => t.Combatant3)
                    .Include(t => t.Combatant4)
                    .Include(t => t.Combatant5)
                    .OrderByDescending(t => t.Score)
                    .ToList()
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Battle()
        {
            var model = new BattleFormViewModel
            {
                CurrentUser = GetCurrentUser(),
                Teams = _context.Teams.OrderBy(t => t.Name).ToList()
            };

            if (model.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Account" });
            }

            return View("BattleForm", model);
        }

        [HttpPost]
        public ViewResult Battle(BattleFormViewModel battleFormViewModel)
        { //Pass in winning teams ID and add to their score. +10 for win -3 for loss
            battleFormViewModel.CurrentUser = GetCurrentUser();

            if (battleFormViewModel.Team1Id == battleFormViewModel.Team2Id)
            {
                ModelState.AddModelError("", "Please select 2 different teams.");
            }

            var team1 = _context.Teams.Find(battleFormViewModel.Team1Id);
            var team2 = _context.Teams.Find(battleFormViewModel.Team2Id);

            if (team1 == null || team2 == null)
            {
                ModelState.AddModelError("", "At least one of those teams does not exist.");
            }

            if (!ModelState.IsValid)
            {
                battleFormViewModel.Teams = _context.Teams.OrderBy(t => t.Name).ToList();
                return View("BattleForm", battleFormViewModel);
            }

            // Battle calculations

            Boolean team1Won = (new Random()).Next(1) == 0;

            if (team1Won)
            {
                team1!.Score += 10;
                team2!.Score -= 3;
            }
            else
            {
                team2!.Score += 10;
                team1!.Score -= 3;
            }

            _context.Update(team1);
            _context.Update(team2);
            _context.SaveChanges();

            var model = new BattleResultViewModel()
            {
                CurrentUser = GetCurrentUser(),
                Winner = team1Won ? team1 : team2,
                Loser = team1Won ? team2 : team1,
                CombatLog = new List<string>(["Smash", "Bash", "Clash"])
            };

            return View("BattleResult", model);
        }
    }
}
