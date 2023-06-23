using WebAppBackend.Entidades;

namespace WebAppBackend.Data.Interfaces
{
    public interface IUsuarioRepository
    {
        public Usuario Login(string login, string pass);
    }
}
