namespace CombatGameSite.Models
{
    public class BattleResultViewModel
    {
        public User? CurrentUser { get; set; }
        public Team? Winner { get; set; }
        public Team? Loser { get; set; }
        public List<string>? CombatLog { get; set; }
    }
}
