using WebAppBackend.Data.Interfaces;
using WebAppBackend.Entidades;

namespace WebAppBackend.Data
{
    public class ClienteRepository : BaseRepo<Cliente>, IClienteRepository
    {
        public ClienteRepository() { }

        public async Task AtualizarCliente(Cliente cliente)
        {
            await this.AdicionarAsync(cliente);
        }

        public async Task InserirCliente(Cliente cliente)
        {
            cliente.DataAtualizacao = DateTime.Now;
            await this.EditarAsync(cliente);
        }

        public async Task<Cliente> ObterClientePorId(int id)
        {
            var cliente = this.Obter(x => x.Id == id);

            return cliente;
        }

        public async Task<List<Cliente>> ObterClientes()
        {
            var clientes = await this.Listar(x=>x.Id > 0, null);

            return clientes.ToList();
        }

        public async Task RemoverCliente(Cliente cliente)
        {
            throw new NotImplementedException();
        }
    }
}
