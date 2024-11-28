using System.ComponentModel.DataAnnotations;

namespace CombatGameSite.Models
{
    public class TeamEditViewModel
    {
        public User? CurrentUser { get; set; }
        public string? Mode { get; set; }
        public List<Combatant>? Combatants { get; set; }
        public Team? Team { get; set; }
    }
}
