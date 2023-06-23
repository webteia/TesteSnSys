using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppBackend.Cache;
using WebAppBackend.Entidades;
using WebAppBackend.Services.Interfaces;

namespace WebAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly ICacheService _cacheService;

        public ClienteController(IClienteService clienteService, ICacheService cacheService)
        {
            _clienteService = clienteService;
            _cacheService = cacheService;   
        }

        [HttpGet]
        [Route("ClienteList")]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            var clienteCache = new List<Cliente>();
            clienteCache = _cacheService.GetData<List<Cliente>>("Cliente");
            if (clienteCache == null)
            {
                var clientes = await _clienteService.ObterClientes();
                if(clientes.Count > 0)
                {
                    clienteCache = clientes;
                    var expiracaoTempo = DateTimeOffset.Now.AddMinutes(3.0);
                    _cacheService.SetData("Cliente", clienteCache, expiracaoTempo);
                }
            }

            return clienteCache;
        }

        [HttpGet]
        [Route("ClienteDetalhe")]
        public async Task<ActionResult<Cliente>> Get(int id)
        {
            var clienteCache = new Cliente();
            var clienteCacheList = new List<Cliente>();

            clienteCacheList = _cacheService.GetData<List<Cliente>>("Cliente");
            clienteCache = clienteCacheList.Find(x => x.Id == id);
            if (clienteCache == null)
            {
                clienteCache = await _clienteService.ObterClientePorId(id);
            }

            return clienteCache;
        }

        [HttpPost]
        [Route("CriarCliente")]
        public async Task<ActionResult<Cliente>> Post(Cliente cliente)
        {
            await _clienteService.InserirCliente(cliente);
            _cacheService.RemoverData("Cliente");

            return CreatedAtAction(nameof(Get), new {id = cliente.Id}, cliente);
        }

        [HttpDelete]
        [Route("RemoverCliente")]
        public async Task<ActionResult<IEnumerable<Cliente>>> Delete(int id)
        {
            var cliente = await _clienteService.ObterClientePorId(id);
            if(cliente == null)
                return NotFound();


            await _clienteService.RemoverCliente(cliente);
            _cacheService.RemoverData("Cliente");

            var clientes = await _clienteService.ObterClientes();
            return Ok(clientes);
        }

        [HttpPost]
        [Route("AtualizarCliente")]
        public async Task<ActionResult<Cliente>> Atualizar(int id, Cliente cliente)
        {
            if(id != cliente.Id)
            {
                return BadRequest();
            }

            var clienteData = await _clienteService.ObterClientePorId(id);
            if(clienteData == null)
            {
                return NotFound();
            }
            await _clienteService.AtualizarCliente(cliente);
            _cacheService.RemoverData("Cliente");

            var clientes = await _clienteService.ObterClientes();
            return Ok(clientes);
        }
    }
}
