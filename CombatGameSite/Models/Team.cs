using System.ComponentModel.DataAnnotations;

namespace CombatGameSite.Models
{ //Model for holding Team data
    public class Team
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Please enter a team name.")]
        [StringLength(50, ErrorMessage = "Team name must not exceed 50 characters.")]
        public string? Name { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }

        public int? Score { get; set; }

        public int? Character1Id { get; set; }
        public Character? Character1 { get; set; }

        public int? Character2Id { get; set; }
        public Character? Character2 { get; set; }

        public int? Character3Id { get; set; }
        public Character? Character3 { get; set; }

        public int? Character4Id { get; set; }
        public Character? Character4 { get; set; }

        public int? Character5Id { get; set; }
        public Character? Character5 { get; set; }

        public List<int> CharacterIds =>
            new List<int?> { Character1Id, Character2Id, Character3Id, Character4Id, Character5Id }
                .Where(i => i != null)
                .Select(i => (int)i!)
                .ToList();
        //Hold a list of all characterIds. Ex => {1,2,3,4,5}
    }
}
