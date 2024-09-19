using System.ComponentModel.DataAnnotations;

namespace ClientesService.Application.DTOs;

public class ClienteDto
{
    [Required(ErrorMessage = "O campo CPF é obrigatório.")]
    [RegularExpression(@"\d{11}", ErrorMessage = "CPF inválido.")]
    public string CPF { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo Endereço é obrigatório.")]
    public string Endereco { get; set; }

    [Required(ErrorMessage = "O campo Profissão é obrigatório.")]
    public string Profissao { get; set; }
}
