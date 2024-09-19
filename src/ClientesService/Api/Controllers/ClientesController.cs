using ClientesService.Application.DTOs;
using ClientesService.Application.Services;
using ClientesService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientesService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet("{cpf}")]
        [ProducesResponseType(typeof(Cliente), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCliente(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                return BadRequest("CPF é obrigatório.");
            }

            var cliente = await _clienteService.ObterPorCpfAsync(cpf);
            if (cliente == null)
                return NotFound($"Cliente com CPF {cpf} não encontrado.");

            return Ok(cliente);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Cliente), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCliente([FromBody] ClienteDto clienteDto)
        {
            if (clienteDto == null)
            {
                return BadRequest("Dados do cliente são obrigatórios.");
            }

            
            if (string.IsNullOrWhiteSpace(clienteDto.CPF) ||
                string.IsNullOrWhiteSpace(clienteDto.Nome) ||
                string.IsNullOrWhiteSpace(clienteDto.Endereco) ||
                string.IsNullOrWhiteSpace(clienteDto.Profissao))
            {
                return BadRequest("Todos os campos são obrigatórios.");
            }

            var cliente = await _clienteService.CriarAsync(clienteDto);
            return CreatedAtAction(nameof(GetCliente), new { cpf = cliente.CPF }, cliente);
        }

    }
}
