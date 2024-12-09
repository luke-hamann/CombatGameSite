using CombatGameSite.Models;
using System.ComponentModel.DataAnnotations;

namespace CombatGameSite.Areas.Account.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter a username.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        public string? PasswordConfirm { get; set; }

        public string? Tagline { get; set; }

        public User? CurrentUser { get; set; }

        public User ToUser()
        {
            return new User()
            {
                Id = 0,
                Name = Username ?? "",
                Password = Password ?? "",
                Tagline = Tagline ?? ""
            };
        }
    }
}
