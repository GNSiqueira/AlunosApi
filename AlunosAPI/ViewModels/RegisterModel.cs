using System.ComponentModel.DataAnnotations;

namespace AlunosAPI.ViewModels
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public required string Email {get; set; }

        [Required]
        [DataType(DataType.Password)]

        public required string Password {get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "Senha não conferem")]

        public required string ConfirmPassword {get; set; }
    }
}
