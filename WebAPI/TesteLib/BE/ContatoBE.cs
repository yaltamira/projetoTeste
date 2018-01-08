using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteLib.BE
{
    [Table("Contato")]
    public class ContatoBE
    {
        [Key]
        public int IdContato { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(120)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [MaxLength(300)]
        public string Email { get; set; }

        [Required]
        public int DDD { get; set; }

        [Required]
        [MaxLength(9)]
        public string Telefone { get; set; }

        public bool Ativo { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual UsuarioBE Usuario { get; set; }
    }
}
