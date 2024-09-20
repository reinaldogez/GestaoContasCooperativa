using ContasService.Domain.Entities;
using System.Threading.Tasks;

namespace ContasService.Domain.Repositories;

public interface IContaRepository
{
    Task<ContaBancaria> CriarContaAsync(ContaBancaria conta);
    Task<ContaBancaria> ObterPorNumeroContaEAgenciaAsync(string numeroConta, string numeroAgencia);
    Task<ContaBancaria> SacarAsync(string numeroConta, string numeroAgencia, decimal valor);
    Task<ContaBancaria> DepositarAsync(string numeroConta, string numeroAgencia, decimal valor);
    Task<ContaCorrente> CalcularJurosAsync(string numeroConta, string numeroAgencia, decimal taxa);
    Task<ContaPoupanca> CalcularRendimentosAsync(string numeroConta, string numeroAgencia, decimal taxa);

}

