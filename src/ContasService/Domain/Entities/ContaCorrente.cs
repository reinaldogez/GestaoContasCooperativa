using ContasService.Domain.Exceptions;

namespace ContasService.Domain.Entities;

public class ContaCorrente : ContaBancaria
{
    public decimal Limite { get; private set; }

    public ContaCorrente(string numeroConta, string numeroAgencia, decimal limite)
        : base(numeroConta, numeroAgencia)
    {
        Limite = limite;
    }

    public override void Sacar(decimal valor)
    {
        if (valor <= 0)
            throw new ValorInvalidoException("O valor do saque deve ser positivo.");

        if (Saldo - valor < -Limite)
            throw new SaldoInsuficienteException();

        Saldo -= valor;
    }

    public void CalcularJuros(decimal taxa)
    {
        if (Saldo < 0) // Calcular juros apenas se o saldo for negativo
        {
            var juros = Saldo * taxa; // Cálculo do valor dos juros com base no saldo negativo
            Saldo -= juros; // Aplica o juros ao saldo negativo (diminuindo ainda mais o saldo)
        }
    }
}

