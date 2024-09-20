using ContasService.Domain.Entities;
using ContasService.Domain.Repositories;
using ContasService.Infrastructure.Data.InMemory.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ContasService.Infrastructure.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private readonly ContasContext _context;

        public ContaRepository(ContasContext context)
        {
            _context = context;
        }

        public async Task<ContaBancaria> CriarContaAsync(ContaBancaria conta)
        {
            _context.Contas.Add(conta);
            await _context.SaveChangesAsync();
            return conta;
        }

        public async Task<ContaBancaria> ObterPorNumeroContaEAgenciaAsync(string numeroConta, string numeroAgencia)
        {
            var conta = await _context.Contas
                .FirstOrDefaultAsync(c => c.NumeroConta == numeroConta && c.NumeroAgencia == numeroAgencia);

            if (conta == null)
                throw new InvalidOperationException("Conta não encontrada.");

            return conta;
        }

        public async Task<ContaBancaria> SacarAsync(string numeroConta, string numeroAgencia, decimal valor)
        {
            var conta = _context.Contas.FirstOrDefault(c => c.NumeroConta == numeroConta && c.NumeroAgencia == numeroAgencia);
            if (conta != null)
            {
                conta.Sacar(valor);
                await _context.SaveChangesAsync();
            }
            return conta;
        }

        public async Task<ContaBancaria> DepositarAsync(string numeroConta, string numeroAgencia, decimal valor)
        {
            var conta = _context.Contas.FirstOrDefault(c => c.NumeroConta == numeroConta && c.NumeroAgencia == numeroAgencia);
            if (conta != null)
            {
                conta.Depositar(valor);
                await _context.SaveChangesAsync();
            }
            return conta;
        }

        public async Task<ContaCorrente> CalcularJurosAsync(string numeroConta, string numeroAgencia, decimal taxa)
        {
            var conta = _context.Contas.FirstOrDefault(c => c.NumeroConta == numeroConta && c.NumeroAgencia == numeroAgencia);

            if (conta == null)
                throw new InvalidOperationException("Conta não encontrada.");

            if (conta is ContaCorrente contaCorrente)
            {
                contaCorrente.CalcularJuros(taxa);
                await _context.SaveChangesAsync();
                return (ContaCorrente)conta;
            }

            throw new InvalidOperationException("Juros só podem ser aplicados a contas correntes.");
        }

        public async Task<ContaPoupanca> CalcularRendimentosAsync(string numeroConta, string numeroAgencia, decimal taxa)
        {
            var conta = _context.Contas.FirstOrDefault(c => c.NumeroConta == numeroConta && c.NumeroAgencia == numeroAgencia);

            if (conta == null)
                throw new InvalidOperationException("Conta não encontrada.");

            if (conta is ContaPoupanca contaPoupanca)
            {
                contaPoupanca.CalcularRendimento(taxa);
                await _context.SaveChangesAsync();
                return (ContaPoupanca)conta;
            }

            throw new InvalidOperationException("Rendimentos só podem ser aplicados a contas poupança.");
        }


    }
}
