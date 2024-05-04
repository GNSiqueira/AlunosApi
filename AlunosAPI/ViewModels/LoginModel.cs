using System.ComponentModel.DataAnnotations;

namespace AlunosAPI.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email é obirgatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public required string Email {get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(20, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo  {1} caracteres.", MinimumLength = 10)]
        [DataType(DataType.Password)]
        public required string Password {get; set; }
    }
}