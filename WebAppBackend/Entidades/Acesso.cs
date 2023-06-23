using System.ComponentModel.DataAnnotations.Schema;
using WebAppBackend.Enum;

namespace WebAppBackend.Entidades
{
    public class Acesso:BaseEntidade
    {
        public AcessosCRUD Permissao { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }

        public int UsuarioId { get; set; }
    }
}
