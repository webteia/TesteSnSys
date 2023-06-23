using WebAppBackend.Entidades;

namespace WebAppBackend.Services.Interfaces
{
    public interface ILoginService
    {
        Usuario Login(string? username, string? password);
    }
}
