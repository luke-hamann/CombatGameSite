namespace CombatGameSite.Models
{
    public class Combatant
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public string? Name { get; set; }
        public int? Health { get; set; }
        public int? TypeId { get; set; }
        public int? Defense { get; set; }
        public string? Species { get; set; }
        public string? SkillPrimaryId { get; set; }
        public Skill? SkillPrimary { get; set; }
        public string? SkillSecondaryId { get; set; }
        public Skill? SkillSecondary { get; set; }
        public string? SkillTertiaryId { get; set; }   
        public Skill? SkillTertiary { get; set; }
    }
}
