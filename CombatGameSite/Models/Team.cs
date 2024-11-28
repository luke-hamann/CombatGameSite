using System.ComponentModel.DataAnnotations;

namespace CombatGameSite.Models
{
    public class Team
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Please enter a team name.")]
        public string? Name { get; set; }


        public int? UserId { get; set; }
        public User? User { get; set; }
        public int? Score { get; set; }
        public int? Combatant1Id { get; set; }
        public Combatant? Combatant1 { get; set; }
        public int? Combatant2Id { get; set; }
        public Combatant? Combatant2 { get; set; }
        public int? Combatant3Id { get; set; }
        public Combatant? Combatant3 { get; set; }
        public int? Combatant4Id { get; set; }
        public Combatant? Combatant4 { get; set; }
        public int? Combatant5Id { get; set; }
        public Combatant? Combatant5 { get; set; }

        public List<int> CombatantIds {
            get
            {
                return (new List<int?> { Combatant1Id, Combatant2Id, Combatant3Id, Combatant4Id, Combatant5Id })
                    .Where(i => i != null)
                    .Select(i => (int)i!)
                    .ToList();
            }
        }
    }
}
