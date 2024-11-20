using CombatGameSite.Models;
using System.ComponentModel.DataAnnotations;

namespace CombatGameSite.Areas.Account.Models
{
    public class LoginViewModel
    {
        public LoginViewModel() { }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Password { get; set; }

        public User? CurrentUser { get; set; }
    }
}
