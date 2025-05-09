using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataMySql.Context;

public class StartRiderContext(DbContextOptions<StartRiderContext> optionsBuilder) : DbContext(optionsBuilder)
{
    public DbSet<Veiculo> Veiculos { get; set; }
    public DbSet<Locatario> Locatarios { get; set; }
    public DbSet<Locacao> Locacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Veiculo>()
            .HasOne(v => v.Locacao)
            .WithOne(l => l.Veiculo)
            .HasForeignKey<Locacao>(l => l.VeiculoId);
        
        modelBuilder.Entity<Locatario>()
            .HasOne(l => l.Locacao)
            .WithOne(l => l.Locatario)
            .HasForeignKey<Locacao>(l => l.LocatarioId);
    }
}