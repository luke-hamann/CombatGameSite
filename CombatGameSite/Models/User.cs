using System.ComponentModel.DataAnnotations;

namespace CombatGameSite.Models
{ //Model for holding User data
    public class User
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Password { get; set; }

        [StringLength(50, ErrorMessage = "Tagline must be 50 characters or less.")]
        public string? Tagline { get; set; }

        [StringLength(50, ErrorMessage = "Favorite book must be 50 characters or less.")]
        public string? FavoriteBook { get; set; }

        [StringLength(50, ErrorMessage = "Favorite game must be 50 characters or less.")]
        public string? FavoriteGame { get; set; }

        [StringLength(50, ErrorMessage = "Favorite movie must be 50 characters or less.")]
        public string? FavoriteMovie { get; set; }
    }
}
