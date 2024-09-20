using ContasService.Domain.Exceptions;

namespace ContasService.Domain.Entities;

public class ContaPoupanca : ContaBancaria
{
    public ContaPoupanca(string numeroConta, string numeroAgencia)
        : base(numeroConta, numeroAgencia)
    {
    }

    public override void Sacar(decimal valor)
    {
        if (valor <= 0)
            throw new ValorInvalidoException("O valor do saque deve ser positivo.");

        if (Saldo < valor)
            throw new SaldoInsuficienteException();

        Saldo -= valor;
    }

    public void CalcularRendimento(decimal taxa)
    {
        if (Saldo > 0)
        {
            Saldo += Saldo * taxa;
        }
    }
}

