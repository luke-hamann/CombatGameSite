namespace CombatGameSite.Models
{
    public class CharacterEditViewModel
    {
        public User? CurrentUser { get; set; }
        public string? Mode { get; set; }
        public Combatant? Combatant { get; set; }
        public List<Skill>? Skills { get; set; }
    }
}
