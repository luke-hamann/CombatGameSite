namespace CombatGameSite.Models
{
    public class CharacterDeleteViewModel
    {
        public User? CurrentUser { get; set; }
        public Character? Character { get; set; }
        public List<Team>? Teams { get; set; }
    }
}
