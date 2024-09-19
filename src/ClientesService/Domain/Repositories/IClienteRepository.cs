using ClientesService.Domain.Entities;
using System.Threading.Tasks;

namespace ClientesService.Domain.Repositories;

public interface IClienteRepository
{
    Task<Cliente> ObterPorCpfAsync(string cpf);
    Task CriarClienteAsync(Cliente cliente);
    
}
