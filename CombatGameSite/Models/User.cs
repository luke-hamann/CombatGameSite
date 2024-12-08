using System.ComponentModel.DataAnnotations;

namespace CombatGameSite.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        [StringLength(50)]
        public string? Tagline { get; set; }

        [StringLength(50)]
        public string? FavoriteBook { get; set; }

        [StringLength(50)]
        public string? FavoriteGame { get; set; }

        [StringLength(50)]
        public string? FavoriteMovie { get; set; }
    }
}
