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
        {//Function to get user based on Session Data
            return _context.Users.Find(HttpContext.Session.GetInt32("userId"));
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {//Default page if no routing is given
            return View(new { CurrentUser = GetCurrentUser() });
        }

        [HttpGet]
        [Route("/leaderboard/")]
        public IActionResult Leaderboard()
        { //Views the leaderboard page
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
            //Return the leaderboard.cshtml with the model passed to it.
            return View(model);
        }

        [HttpGet]
        [Route("/battle/")]
        public ViewResult Battle()
        {// Send a list of ordered teams (as determined by CurrentUser) to the BattleForm.cshtml
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
        { //Conduct a battle between two teams. 
            battleFormViewModel.CurrentUser = GetCurrentUser();

            if (battleFormViewModel.Team1Id == battleFormViewModel.Team2Id)
            {//Check that the same team hasen't been chosen twice. Display an error if it has happened.
                ModelState.AddModelError("", "Please select 2 different teams.");
            }

            var team1 = _context.Teams.Find(battleFormViewModel.Team1Id);
            var team2 = _context.Teams.Find(battleFormViewModel.Team2Id);

            if (team1 == null || team2 == null)
            {//Verify both teams exist.
                ModelState.AddModelError("", "At least one of those teams does not exist.");
            }

            if (!ModelState.IsValid)
            {//If the model state is invalid, send the model to BattleForm.cshtml for reselection
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
            //Update and save battle changes
            _context.Update(team1);
            _context.Update(team2);
            _context.SaveChanges();

            //Prep a model to be passed into BattleResult.cshtml
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
