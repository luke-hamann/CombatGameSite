namespace CombatGameSite.Models
{
    public class UserCombatantsViewModel
    {
        public User? CurrentUser { get; set; }
        public required User SelectedUser { get; set; }
        public required List<Combatant> Combatants { get; set; }
    }
}
