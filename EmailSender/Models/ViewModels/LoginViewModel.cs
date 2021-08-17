using System.ComponentModel.DataAnnotations;

namespace EmailSender.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Czy mam zapamiętać twoje dane?")]
        public bool RememberMe { get; set; }
    }
}
