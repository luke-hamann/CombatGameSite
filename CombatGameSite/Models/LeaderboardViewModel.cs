namespace CombatGameSite.Models
{
    public class LeaderboardViewModel
    {
        public User? CurrentUser { get; set; }
        public List<Team> Teams { get; set; }
    }
}
