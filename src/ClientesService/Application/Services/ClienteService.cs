using ClientesService.Domain.Entities;
using ClientesService.Domain.Repositories;
using ClientesService.Application.DTOs;
using System.Threading.Tasks;

namespace ClientesService.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<Cliente> ObterPorCpfAsync(string cpf)
    {
        return await _clienteRepository.ObterPorCpfAsync(cpf);
    }

    public async Task<Cliente> CriarAsync(ClienteDto clienteDto)
    {
        var cliente = new Cliente
        {
            CPF = clienteDto.CPF,
            Nome = clienteDto.Nome,
            Endereco = clienteDto.Endereco,
            Profissao = clienteDto.Profissao
        };

        await _clienteRepository.CriarClienteAsync(cliente);
        return cliente;
    }
}
