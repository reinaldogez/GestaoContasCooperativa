using Microsoft.AspNetCore.Mvc;
using ContasService.Application.DTOs;
using ContasService.Application.Services;
using ContasService.Domain.Entities;
using ContasService.Domain.Exceptions;
using System.Threading.Tasks;

namespace ContasService.Api.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
public class ContasController : ControllerBase
{
    private readonly IContaService _contaService;

    public ContasController(IContaService contaService)
    {
        _contaService = contaService;
    }

    [HttpPost("criar")]
    [ProducesResponseType(typeof(ContaBancaria), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> CriarConta([FromBody] ContaCriacaoDto contaCriacaoDto)
    {
        if (contaCriacaoDto == null)
        {
            return BadRequest("Os dados da conta são obrigatórios.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var novaConta = await _contaService.CriarContaAsync(contaCriacaoDto);

            return StatusCode(201, novaConta);
        }
        catch (OperacaoInvalidaException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("{numeroConta}/{numeroAgencia}/{cpfCliente}")]
    [ProducesResponseType(typeof(ContaBancaria), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetConta(string numeroConta, string numeroAgencia, string cpfCliente)
    {
        if (string.IsNullOrWhiteSpace(numeroConta))
        {
            return BadRequest("Número da conta é obrigatório.");
        }

        try
        {
            var conta = await _contaService.ObterPorNumeroContaAgenciaECpfAsync(numeroConta, numeroAgencia, cpfCliente);
            if (conta == null)
                return NotFound($"Conta com número {numeroConta} não encontrada.");

            return Ok(conta);
        }
        catch (OperacaoInvalidaException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("{numeroConta}/{numeroAgencia}/sacar")]
    [ProducesResponseType(typeof(ContaBancaria), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Sacar(string numeroConta, string numeroAgencia, [FromBody] decimal valor)
    {
        if (valor <= 0)
        {
            return BadRequest("O valor do saque deve ser positivo.");
        }

        try
        {
            var conta = await _contaService.SacarAsync(numeroConta, numeroAgencia, valor);
            if (conta == null)
            {
                return NotFound($"Conta com número {numeroConta} não encontrada.");
            }

            return Ok(conta);
        }
        catch (SaldoInsuficienteException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
        catch (LimiteExcedidoException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
        catch (OperacaoInvalidaException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("{numeroConta}/{numeroAgencia}/depositar")]
    [ProducesResponseType(typeof(ContaBancaria), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Depositar(string numeroConta, string numeroAgencia, [FromBody] decimal valor)
    {
        if (valor <= 0)
        {
            return BadRequest("O valor do depósito deve ser positivo.");
        }

        try
        {
            var conta = await _contaService.DepositarAsync(numeroConta, numeroAgencia, valor);
            if (conta == null)
            {
                return NotFound($"Conta com número {numeroConta} não encontrada.");
            }

            return Ok(conta);
        }
        catch (OperacaoInvalidaException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }


    [HttpPost("{numeroConta}/{numeroAgencia}/calcular-juros")]
    [ProducesResponseType(typeof(ContaBancaria), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> CalcularJuros(string numeroConta, string numeroAgencia, [FromBody] decimal taxa)
    {
        if (taxa <= 0)
        {
            return BadRequest("A taxa de juros deve ser positiva.");
        }

        try
        {
            var conta = await _contaService.CalcularJurosAsync(numeroConta, numeroAgencia, taxa);
            if (conta == null)
            {
                return NotFound($"Conta com número {numeroConta} não encontrada.");
            }

            return Ok(conta);
        }
        catch (OperacaoInvalidaException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}
