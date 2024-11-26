namespace CombatGameSite.Models
{
    public class UserViewModel
    {
        public User? CurrentUser { get; set; }
        public User? SelectedUser { get; set; }
        public string? SelectedSection {  get; set; }
        public List<Combatant>? Combatants { get; set; }
        public List<Team>? Teams { get; set; }
    }
}
