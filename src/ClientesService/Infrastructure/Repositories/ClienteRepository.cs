using ClientesService.Domain.Entities;
using ClientesService.Domain.Repositories;
using ClientesService.Infrastructure.Data.InMemory.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ClientesService.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly ClientesContext _context;

    public ClienteRepository(ClientesContext context)
    {
        _context = context;
    }

    public async Task<Cliente> ObterPorCpfAsync(string cpf)
    {
        return await _context.Clientes.FirstOrDefaultAsync(c => c.CPF == cpf);
    }

    public async Task CriarClienteAsync(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
    }

}
