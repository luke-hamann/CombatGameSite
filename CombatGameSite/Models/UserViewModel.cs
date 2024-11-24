namespace CombatGameSite.Models
{
    public class UserViewModel
    {
        public User? CurrentUser { get; set; }
        public required User SelectedUser { get; set; }
        public required string SelectedSection {  get; set; }
        public required List<Combatant> Combatants { get; set; }
        public required List<Team> Teams { get; set; }
    }
}
