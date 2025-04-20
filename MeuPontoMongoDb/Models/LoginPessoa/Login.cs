using System.ComponentModel.DataAnnotations;

namespace MeuPontoMongoDb.Models.Login
{
    public class Login
    {
        [Required(ErrorMessage = "Email Obrigatorio")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Senha Obrigatoria")]
        public string? Senha { get; set; }  
    }
}
