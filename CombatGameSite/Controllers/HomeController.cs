using CombatGameSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CombatGameSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly CombatContext _context;

        public HomeController(CombatContext context)
        {//Constructor method for controller
            _context = context;
        }

        [NonAction]
        private User? GetCurrentUser()
        {
            // Get the user object for the currently logged in user
            return _context.Users.Find(HttpContext.Session.GetInt32("userId"));
        }

        [HttpGet]
        [Route("/")]
        public ViewResult Index()
        {
            return View(new { CurrentUser = GetCurrentUser() });
        }

        [HttpGet]
        [Route("/leaderboard/")]
        public ViewResult Leaderboard()
        {
            var model = new LeaderboardViewModel()
            { //Setup viewmodel with a current user and Ordered list of teams.
                CurrentUser = GetCurrentUser(),
                Teams = _context.Teams
                    .Include(t => t.User)
                    .Include(t => t.Character1)
                    .Include(t => t.Character2)
                    .Include(t => t.Character3)
                    .Include(t => t.Character4)
                    .Include(t => t.Character5)
                    .OrderByDescending(t => t.Score)
                    .ToList()
            };

            return View(model);
        }

        [HttpGet]
        [Route("/battle/")]
        public ViewResult Battle()
        {
            var model = new BattleFormViewModel
            {
                CurrentUser = GetCurrentUser(),
                Teams = _context.Teams.OrderBy(t => t.Name).ToList()
            };

            return View("BattleForm", model);
        }

        [HttpPost]
        [Route("/battle/")]
        public ViewResult Battle(BattleFormViewModel model)
        {
            model.CurrentUser = GetCurrentUser();

            // Verify two different teams were selected
            if (model.Team1Id == model.Team2Id)
            {
                ModelState.AddModelError("", "Please select 2 different teams.");
            }

            // Verify both teams exist
            var team1 = _context.Teams.Find(model.Team1Id);
            var team2 = _context.Teams.Find(model.Team2Id);

            if (team1 == null || team2 == null)
            {
                ModelState.AddModelError("", "You selected a team that does not exist.");
            }

            // Show the battle form again if validation failed
            if (!ModelState.IsValid)
            {
                model.Teams = _context.Teams.OrderBy(t => t.Name).ToList();
                return View("BattleForm", model);
            }

            // Battle calculations

            bool team1Won = (new Random()).Next(2) == 0;

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

            // Save the changes to the team scores and display the results

            _context.Update(team1);
            _context.Update(team2);
            _context.SaveChanges();

            var result = new BattleResultViewModel()
            {
                CurrentUser = GetCurrentUser(),
                Winner = team1Won ? team1 : team2,
                Loser = team1Won ? team2 : team1,
                CombatLog = new List<string>(["Smash", "Bash", "Clash"])
            };

            return View("BattleResult", result);
        }
    }
}
