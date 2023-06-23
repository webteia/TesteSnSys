using WebAppBackend.Data.Interfaces;
using WebAppBackend.Entidades;
using WebAppBackend.Services.Interfaces;

namespace WebAppBackend.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task AtualizarCliente(Cliente cliente)
        {
            await _clienteRepository.AtualizarCliente(cliente);
        }

        public async Task InserirCliente(Cliente cliente)
        {
            await _clienteRepository.InserirCliente(cliente);
        }

        public async Task<Cliente> ObterClientePorId(int id)
        {
            return await _clienteRepository.ObterClientePorId(id);
        }

        public async Task<List<Cliente>> ObterClientes()
        {
            return await _clienteRepository.ObterClientes();
        }

        public async Task RemoverCliente(Cliente cliente)
        {
            await _clienteRepository.RemoverCliente(cliente);
        }
    }
}
