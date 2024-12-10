    using System.ComponentModel.DataAnnotations;

namespace CombatGameSite.Models
{ //Character model for database
    public class Character
    {
        public const int MAX_SKILL_POINTS = 125;

        public int? Id { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter a health value.")]
        [Range(0, 200, ErrorMessage = "Health must be between 0 and 200.")]
        public int? Health { get; set; }

        [Required(ErrorMessage = "Please enter a species.")]
        public string? Species { get; set; }

        [Required(ErrorMessage = "Please select an element type.")]
        [Range(1, 4, ErrorMessage = "Please select an element type.")]
        public int? TypeId { get; set; }

        public string? SkillPrimaryId { get; set; }
        public Skill? SkillPrimary { get; set; }

        public string? SkillSecondaryId { get; set; }
        public Skill? SkillSecondary { get; set; }

        public string? SkillTertiaryId { get; set; }   
        public Skill? SkillTertiary { get; set; }

        public new string GetType() =>
            TypeId switch
            {
                1 => "Water",
                2 => "Fire",
                3 => "Wind",
                4 => "Earth",
                _ => "",
            }; //Return a type based on the TypeId.

        public bool HasUniqueSkills()
        {
            var skillList = new List<string?>([SkillPrimaryId, SkillSecondaryId, SkillTertiaryId]);
            skillList = skillList.Where(s => s != null).ToList();
            return skillList.Count == skillList.Distinct().ToList().Count;
        } //Return how many skills the character has associated to them.

        public bool HasValidSkillPointDistribution()
        {
            int total = ((Health ?? 0) - 100) / 2 +
                (SkillPrimary?.Cost ?? 0) +
                (SkillSecondary?.Cost ?? 0) +
                (SkillTertiary?.Cost ?? 0);
            return total <= MAX_SKILL_POINTS;
        } //Validates that a character has distributed their skill points correctly.
    }
}
