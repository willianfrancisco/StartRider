using DataMySql.LocacaoRepository;
using DataMySql.LocatarioRepository;
using DataMySql.UsuarioRepository;
using DataMySql.VeiculoRepository;
using Domain.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace DataMySql;

public static class DataMysqlModule
{
     public static IServiceCollection AdicionaDependenciasInfraDataMysql(this IServiceCollection services)
     {
          services.AddScoped<IVeiculoRepositoryPort, VeiculoRepositoryPort>();
          services.AddScoped<ILocatarioRepositoryPort, LocatarioRepositoryPort>();
          services.AddScoped<ILocacaoRepositoryPort, LocacaoRepositoryPort>();
          services.AddScoped<IUsuarioRepositoryPort, UsuarioRepositoryPort>();
          return services;
     }
}