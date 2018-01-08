using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteLib.BE
{
    [Table("Usuario")]
    public class UsuarioBE
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(120)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [MaxLength(300)]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [MaxLength(100)]
        public string Senha { get; set; }

    }
}
