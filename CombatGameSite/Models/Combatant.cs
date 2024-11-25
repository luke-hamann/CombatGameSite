using System.ComponentModel.DataAnnotations;

namespace CombatGameSite.Models
{
    public class Combatant
    {
        public int? Id { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [Range(0, 100)]
        public int? Health { get; set; }

        [Required]
        [Range(1, 4)]
        public int? TypeId { get; set; }

        [Required]
        [Range(0, 100)]
        public int? Defense { get; set; }

        [Required]
        public string? Species { get; set; }

        public string? SkillPrimaryId { get; set; }
        public Skill? SkillPrimary { get; set; }

        public string? SkillSecondaryId { get; set; }
        public Skill? SkillSecondary { get; set; }

        public string? SkillTertiaryId { get; set; }   
        public Skill? SkillTertiary { get; set; }

        public new string GetType()
        {
            switch (TypeId)
            {
                case 1: return "Water";
                case 2: return "Fire";
                case 3: return "Wind";
                case 4: return "Earth";
                default: return "";
            }
        }
    }
}
