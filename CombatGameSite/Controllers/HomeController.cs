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
        public ViewResult Battle(BattleFormViewModel battleFormViewModel)
        {
            battleFormViewModel.CurrentUser = GetCurrentUser();

            // Verify two different teams were selected
            if (battleFormViewModel.Team1Id == battleFormViewModel.Team2Id)
            {//Check that the same team hasen't been chosen twice. Display an error if it has happened.
                ModelState.AddModelError("", "Please select 2 different teams.");
            }

            // Verify both teams exist
            var team1 = _context.Teams.Find(battleFormViewModel.Team1Id);
            var team2 = _context.Teams.Find(battleFormViewModel.Team2Id);

            if (team1 == null || team2 == null)
            {//Verify both teams exist.
                ModelState.AddModelError("", "At least one of those teams does not exist.");
            }

            if (!ModelState.IsValid)
            {
                battleFormViewModel.Teams = _context.Teams.OrderBy(t => t.Name).ToList();
                return View("BattleForm", battleFormViewModel);
            }

            // Battle calculations

            bool team1Won = (new Random()).Next(1) == 0;
            //Coin flip desides the victor. Loser gets -3 points, winner gets +10.
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
