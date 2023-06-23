using System.Collections.ObjectModel;

namespace WebAppBackend.Entidades
{
    public class Usuario : BaseEntidade
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual Collection<Acesso> Acessos { get; set; }
    }
}
