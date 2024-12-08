namespace CombatGameSite.Models
{
    public class TeamEditViewModel
    {
        public User? CurrentUser { get; set; }
        public string? Mode { get; set; }
        public List<Character>? Characters { get; set; }
        public Team? Team { get; set; }
    }
}
