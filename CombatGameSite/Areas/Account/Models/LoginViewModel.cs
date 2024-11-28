using CombatGameSite.Models;
using System.ComponentModel.DataAnnotations;

namespace CombatGameSite.Areas.Account.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your username.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        public string? Password { get; set; }

        public User? CurrentUser { get; set; }
    }
}
