using CombatGameSite.Models;
using System.ComponentModel.DataAnnotations;

namespace CombatGameSite.Areas.Account.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? PasswordConfirm { get; set; }

        public User? currentUser { get; set; }
    }
}
