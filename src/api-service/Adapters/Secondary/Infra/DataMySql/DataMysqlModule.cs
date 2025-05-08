using DataMySql.VeiculoRepository;
using Domain.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace DataMySql;

public static class DataMysqlModule
{
     public static IServiceCollection AdicionaDependenciasInfraDataMysql(this IServiceCollection services)
     {
          services.AddScoped<IVeiculoRepositoryPort, VeiculoRepositoryPort>();
          return services;
     }
}