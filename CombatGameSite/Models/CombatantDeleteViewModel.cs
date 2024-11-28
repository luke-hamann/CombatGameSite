namespace CombatGameSite.Models
{
    public class CombatantDeleteViewModel
    {
        public User? CurrentUser { get; set; }
        public Combatant? Combatant { get; set; }
        public List<Team>? Teams { get; set; }
    }
}
