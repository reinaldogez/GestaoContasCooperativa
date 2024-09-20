namespace ContasService.Domain.Exceptions;


public class ValorInvalidoException : Exception
{
    public ValorInvalidoException(string mensagem) : base(mensagem) { }
}
