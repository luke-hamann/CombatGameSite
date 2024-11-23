namespace CombatGameSite.Models
{
    public class BattleFormViewModel
    {
        public User? CurrentUser { get; set; }
        public List<Team>? Teams { get; set; }
        public int Team1Id { get; set; }
        public int Team2Id { get; set; }
    }
}
