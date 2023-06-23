using WebAppBackend.Entidades;

namespace WebAppBackend.Data.Interfaces
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> ObterClientes();
        Task<Cliente> ObterClientePorId(int id);
        Task InserirCliente(Cliente cliente);
        Task RemoverCliente(Cliente cliente);
        Task AtualizarCliente(Cliente cliente);
    }
}
