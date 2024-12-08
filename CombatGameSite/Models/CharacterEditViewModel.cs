namespace CombatGameSite.Models
{
    public class CharacterEditViewModel
    {
        public User? CurrentUser { get; set; }
        public string? Mode { get; set; }
        public Character? Character { get; set; }
        public List<Skill>? Skills { get; set; }

        public List<string>? SkillIds { get; set; }

        public void SetSkills()
        {
            Character!.SkillPrimaryId = SkillIds?.ElementAtOrDefault(0);
            Character.SkillSecondaryId = SkillIds?.ElementAtOrDefault(1);
            Character.SkillTertiaryId = SkillIds?.ElementAtOrDefault(2);
        }
    }
}
