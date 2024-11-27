using System.ComponentModel.DataAnnotations;

namespace CombatGameSite.Models
{
    public class TeamEditViewModel
    {
        public User? CurrentUser { get; set; }
        public string? Mode { get; set; }
        public List<Combatant>? Combatants { get; set; }

        [Required(ErrorMessage = "Please enter a team name.")]
        public string TeamName { get; set; }

        public int? Combatant1Id { get; set; }
        public int? Combatant2Id { get; set; }
        public int? Combatant3Id { get; set; }
        public int? Combatant4Id { get; set; }
        public int? Combatant5Id { get; set; }

        public Team GetTeam()
        {
            return new Team()
            {
                Name = TeamName,
                UserId = CurrentUser.Id,
                Score = 0,
                Combatant1Id = Combatant1Id,
                Combatant2Id = Combatant2Id,
                Combatant3Id = Combatant3Id,
                Combatant4Id = Combatant4Id,
                Combatant5Id = Combatant5Id,
            };
        }
    }
}
