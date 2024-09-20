namespace ContasService.Domain.Exceptions;

public class OperacaoInvalidaException : Exception
{
    public OperacaoInvalidaException(string message)
        : base(message)
    {
    }
}