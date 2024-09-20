using ContasService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContasService.Infrastructure.Data.InMemory.DataAccess
{
    public class ContasContext : DbContext
    {
        public ContasContext(DbContextOptions<ContasContext> options) : base(options) { }

        public DbSet<ContaBancaria> Contas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContaBancaria>()
                .HasKey(c => new { c.NumeroConta, c.NumeroAgencia });


            modelBuilder.Entity<ContaCorrente>();
            modelBuilder.Entity<ContaPoupanca>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
