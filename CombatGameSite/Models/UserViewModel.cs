namespace CombatGameSite.Models
{
    public class UserViewModel
    {
        public User? CurrentUser { get; set; }
        public User? SelectedUser { get; set; }
        public List<Character>? Characters { get; set; }
        public List<Team>? Teams { get; set; }
    }
}
