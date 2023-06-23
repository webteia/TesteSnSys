using WebAppBackend.Data.Interfaces;
using WebAppBackend.Entidades;

namespace WebAppBackend.Data
{
    public class UsuarioRepository: BaseRepo<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository() { }

        public Usuario Login(string login, string pass)
        {
            var usuarioLogon = Db.Set<Usuario>()
                .FirstOrDefault(u => u.Login == login && u.Password == pass);

            return usuarioLogon;
        }
    }
}
