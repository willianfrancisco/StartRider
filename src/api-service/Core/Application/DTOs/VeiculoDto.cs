using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs;

public record LerVeiculoDto(
    int Id, 
    string Marca,
    string Modelo,
    string Placa, 
    string NumeroRenavam, 
    ETipoVeiculo TipoVeiculo, 
    ETipoCombustivel TipoCombustivel, 
    string AnoFabricacaoModelo,
    string Cor, 
    string UltimoLicenciamento);

public record NovoVeiculoDto(
    string Marca,
    string Modelo,
    string Placa, 
    string NumeroRenavam, 
    ETipoVeiculo TipoVeiculo, 
    ETipoCombustivel TipoCombustivel, 
    string AnoFabricacaoModelo,
    string Cor, 
    string UltimoLicenciamento);

public record AtualizaVeiculoDto(
    string? Placa,
    string? NumeroRenavam,
    string? Cor,
    string? Modelo
    );

public static class VeiculoDtoAdapter
{
    public static LerVeiculoDto ConverterParaLerVeiculoDto(this Veiculo veiculo)
    {
        return new LerVeiculoDto(
            veiculo.Id, 
            veiculo.Marca,
            veiculo.Modelo,
            veiculo.Placa, 
            veiculo.NumeroRenavam, 
            veiculo.TipoVeiculo, 
            veiculo.TipoCombustivel, 
            veiculo.AnoFabricacaoModelo,
            veiculo.Cor,
            veiculo.UltimoLicenciamento
            );
    }

    public static List<LerVeiculoDto> ConverterParaListaLerVeiculoDto(this List<Veiculo> veiculos)
    {
        return veiculos.Select(v => v.ConverterParaLerVeiculoDto()).ToList();
    }
}