using Domain.Entities;

namespace Application.DTOs;

public record NovaLocacaoDto(
    DateTime DataRetirada,
    int Plano,
    int VeiculoId,
    int LocatarioId
    );
    
public record LerLocacaoDto(
    int Id,
    int Plano,
    int VeiculoId,
    int LocatarioId,
    DateTime DataRetirada,
    DateTime DataDevolucao,
    DateTime DataEstimadaDevolucao,
    double ValorDiariaPlano,
    double ValorTotalLocaco
    );

public record AtualizaDataDevolucaoLocacaoDto(
    DateTime DataDevolucao
    );

public static class LocacaoDtoAdapters
{
    public static LerLocacaoDto ConverterParaLerLocacaoDto(this Locacao locacao)
    {
        return new LerLocacaoDto(locacao.Id,locacao.Plano,locacao.VeiculoId,locacao.LocatarioId,locacao.DataRetirada,locacao.DataDevolucao,locacao.DataEstimadaDevolucao,locacao.ValorDiariaPlano,locacao.ValorTotalLocaco);
    }

    public static Locacao ConverterParaLocacaoEntity(this NovaLocacaoDto locacaodto)
    {
        return new Locacao(locacaodto.DataRetirada, locacaodto.Plano, locacaodto.VeiculoId,locacaodto.LocatarioId);
    }
}