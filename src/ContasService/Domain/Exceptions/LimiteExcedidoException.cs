namespace ContasService.Domain.Exceptions;

public class LimiteExcedidoException : Exception
{
    public LimiteExcedidoException()
        : base("O valor do saque excede o limite da conta.")
    {
    }
}
