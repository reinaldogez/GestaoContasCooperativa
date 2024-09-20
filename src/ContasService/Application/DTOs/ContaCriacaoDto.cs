using System.ComponentModel.DataAnnotations;

namespace ContasService.Application.DTOs;

public enum TipoConta
{
    Corrente,
    Poupanca
}

public class ContaCriacaoDto
    {
        [Required(ErrorMessage = "O número da conta é obrigatório.")]
        [RegularExpression(@"\d{5,10}", ErrorMessage = "O número da conta deve ter entre 5 e 10 dígitos.")]
        public string NumeroConta { get; set; }

        [Required(ErrorMessage = "O número da agência é obrigatório.")]
        [RegularExpression(@"\d{4,6}", ErrorMessage = "O número da agência deve ter entre 4 e 6 dígitos.")]
        public string NumeroAgencia { get; set; }

        [Required(ErrorMessage = "O CPF do cliente é obrigatório.")]
        [RegularExpression(@"\d{11}", ErrorMessage = "CPF inválido. Deve conter 11 dígitos.")]
        public string CpfCliente { get; set; }

        [Required(ErrorMessage = "O tipo de conta é obrigatório.")]
        public TipoConta TipoConta { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "O limite deve ser maior ou igual a zero.")]
        public decimal? Limite { get; set; } // Limite apenas para contas correntes
    }
