using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataMySql.Context;

public class StartRiderContext(DbContextOptions<StartRiderContext> optionsBuilder) : DbContext(optionsBuilder)
{
    public DbSet<Veiculo> Veiculos { get; set; }
    public DbSet<Locatario> Locatarios { get; set; }
}