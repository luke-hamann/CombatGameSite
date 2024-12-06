﻿using System.ComponentModel.DataAnnotations;

namespace CombatGameSite.Models
{
    public class Character
    {
        public const int MAX_SKILL_POINTS = 125;

        public int? Id { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please select an element type.")]
        [Range(1, 4, ErrorMessage = "Please select an element type.")]
        public int? TypeId { get; set; }

        [Required(ErrorMessage = "Please enter a health value.")]
        [Range(0, 200)]
        public int? Health { get; set; }

        [Required(ErrorMessage = "Please enter a species.")]
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

        public bool hasValidSkillPointDistribution()
        {
            int total =(int)(((Health ?? 0) - 100) / 2 +
                (SkillPrimary?.Cost != null ? SkillPrimary.Cost : 0) +
                (SkillSecondary?.Cost != null ? SkillSecondary.Cost : 0) +
                (SkillTertiary?.Cost != null ? SkillTertiary.Cost : 0)
                );
            return total <= MAX_SKILL_POINTS;
        }
    }
}
