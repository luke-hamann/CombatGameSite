namespace CombatGameSite.Models
{
    public class CharacterEditViewModel
    {
        public User? CurrentUser { get; set; }
        public string? Mode { get; set; }
        public Character? Character { get; set; }
        public List<Skill>? Skills { get; set; }

        public List<string> SkillIds { get; set; }

    }
}
