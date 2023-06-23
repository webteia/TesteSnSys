using WebAppBackend.Entidades;

namespace WebAppBackend.Services.Interfaces
{
    public interface IClienteService
    {
        Task<List<Cliente>> ObterClientes();
        Task<Cliente> ObterClientePorId(int id);
        Task InserirCliente(Cliente cliente);
        Task RemoverCliente(Cliente cliente);
        Task AtualizarCliente(Cliente cliente);
    }
}
