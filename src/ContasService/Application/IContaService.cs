using ContasService.Application.DTOs;
using ContasService.Domain.Entities;
using System.Threading.Tasks;

namespace ContasService.Application.Services;

public interface IContaService
{
    Task<ContaBancaria> CriarContaAsync(ContaCriacaoDto contaCriacaoDto);
    Task<ContaBancaria> ObterPorNumeroContaAgenciaECpfAsync(string numeroConta, string numeroAgencia, string cpfCliente);
    Task<ContaBancaria> SacarAsync(string numeroConta, string numeroAgencia, decimal valor);
    Task<ContaBancaria> DepositarAsync(string numeroConta, string numeroAgencia, decimal valor);
    Task<ContaCorrente> CalcularJurosAsync(string numeroConta, string numeroAgencia, decimal taxa);
}

