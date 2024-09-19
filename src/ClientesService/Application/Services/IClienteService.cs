using ClientesService.Domain.Entities;
using ClientesService.Application.DTOs;
using System.Threading.Tasks;

namespace ClientesService.Application.Services;

public interface IClienteService
{
    Task<Cliente> ObterPorCpfAsync(string cpf);
    Task<Cliente> CriarAsync(ClienteDto clienteDto);

}

