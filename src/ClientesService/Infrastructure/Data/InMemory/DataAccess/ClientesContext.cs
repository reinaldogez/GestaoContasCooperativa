using ClientesService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientesService.Infrastructure.Data.InMemory.DataAccess;

public class ClientesContext : DbContext
{
    public ClientesContext(DbContextOptions<ClientesContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>()
            .HasKey(c => c.CPF);

        // Configurações adicionais (opcional)
        modelBuilder.Entity<Cliente>()
            .Property(c => c.Nome)
            .IsRequired();

        modelBuilder.Entity<Cliente>()
            .Property(c => c.Endereco)
            .IsRequired();

        modelBuilder.Entity<Cliente>()
            .Property(c => c.Profissao)
            .IsRequired();
    }
}

