using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Locacao
{
    public Locacao(int id, DateTime dataRetirada, DateTime dataDevolucao, int plano, int veiculoId, int locatarioId)
    {
        Id = id;
        DataRetirada = dataRetirada;
        DataDevolucao = dataDevolucao;
        Plano = plano;
        VeiculoId = veiculoId;
        LocatarioId = locatarioId;
    }

    public Locacao(DateTime dataRetirada, int plano, int veiculoId, int locatarioId)
    {
        DataRetirada = dataRetirada;
        DataDevolucao = dataRetirada.AddDays(plano);
        Plano = plano;
        VeiculoId = veiculoId;
        LocatarioId = locatarioId;
    }

    public int Id { get; private set; }
    
    public DateTime DataRetirada { get; private set; }
    public DateTime DataDevolucao { get; private set; }
    public int Plano { get; private set; }
    [JsonIgnore]
    public int VeiculoId { get; private set; }
    [JsonIgnore]
    public int LocatarioId { get; private set; }
    
    public DateTime DataEstimadaDevolucao => DataRetirada.AddDays(Plano);
    
    public double ValorDiariaPlano => MapValorDiaria(Plano);

    public double ValorTotalLocaco => CalculaValorTotalLocacao();
    public Veiculo Veiculo { get; set; }
    public Locatario Locatario { get; set; }

    private double MapValorDiaria(int plano)
    {
        return plano switch
        {
            7 => 30.00,
            15 => 28.00,
            30 => 22.00,
            45 => 20.00,
            50 => 18.00,
            _ => throw new ArgumentOutOfRangeException(nameof(plano), $"Plano desconhecido: {plano}")
        };
    }

    private double CalculaValorTotalLocacao()
    {
        double valorTotalLocacao = 0.0;

        if (DataDevolucao < DataEstimadaDevolucao)
        {
            var percentualMulta = Plano == 7 ? 0.20 : Plano == 15 ? 0.40 : 1;
            var valorMulta = ValorDiariaPlano * Plano * percentualMulta;
            valorTotalLocacao = (ValorDiariaPlano * Plano) + valorMulta;
        }
        else if (DataDevolucao > DataEstimadaDevolucao)
        {
            var diasEmAtraso = (DataDevolucao - DataEstimadaDevolucao).Days;
            var valorMulta = diasEmAtraso * 50;
            valorTotalLocacao = (ValorDiariaPlano * Plano) + valorMulta;
        }
        else
        {
            valorTotalLocacao = ValorDiariaPlano * Plano;
        }
        
        return valorTotalLocacao;
    }

    public void AtualizaDataDevolucao(DateTime dataDevolucao)
    {
        this.DataDevolucao = dataDevolucao;
    }
}