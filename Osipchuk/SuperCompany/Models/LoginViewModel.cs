using System.ComponentModel.DataAnnotations;

namespace SuperCompany.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name ="Login")]
        public string Username { get; set; }
        [Required]
        [UIHint("password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Rememder Me?")]
        public bool RememberMe { get; set; }

    }
}
