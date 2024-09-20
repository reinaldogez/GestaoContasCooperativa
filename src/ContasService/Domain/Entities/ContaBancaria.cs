using ContasService.Domain.Exceptions;

namespace ContasService.Domain.Entities;

public abstract class ContaBancaria
{
    public string NumeroConta { get; private set; }
    public string NumeroAgencia { get; private set; }
    public decimal Saldo { get; protected set; } // apenas classes que herdem de ContaBancaria podem alterar o saldo
    public string CpfCliente { get; set; }

    public ContaBancaria(string numeroConta, string numeroAgencia)
    {
        if (string.IsNullOrWhiteSpace(numeroConta))
            throw new ArgumentException("O número da conta é obrigatório.", nameof(numeroConta));

        if (string.IsNullOrWhiteSpace(numeroAgencia))
            throw new ArgumentException("O número da agência é obrigatório.", nameof(numeroAgencia));

        NumeroConta = numeroConta;
        NumeroAgencia = numeroAgencia;
        Saldo = 0;
    }

    public void Depositar(decimal valor)
    {
        if (valor <= 0)
            throw new ValorInvalidoException("O valor do depósito deve ser positivo.");
        
        Saldo += valor;
    }

    public abstract void Sacar(decimal valor);
}
