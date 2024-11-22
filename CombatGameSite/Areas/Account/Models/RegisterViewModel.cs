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

        public string? Tagline { get; set; }

        public User? CurrentUser { get; set; }

        public User toUser()
        {
            var user = new User();
            user.Id = 0;
            user.Name = Username ?? "";
            user.Password = Password ?? "";
            user.Tagline = Tagline ?? "";
            return user;
        }
    }
}
