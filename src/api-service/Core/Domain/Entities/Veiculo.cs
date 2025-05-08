using System.Text.Json.Serialization;
using Domain.Enums;

namespace Domain.Entities;

public class Veiculo
{
    public Veiculo(int id, string marca, string modelo, string placa, string numeroRenavam, ETipoVeiculo tipoVeiculo, ETipoCombustivel tipoCombustivel, string anoFabricacaoModelo, string? cor, string ultimoLicenciamento)
    {
        Id = id;
        Marca = marca;
        Modelo = modelo;
        Placa = placa;
        NumeroRenavam = numeroRenavam;
        TipoVeiculo = tipoVeiculo;
        TipoCombustivel = tipoCombustivel;
        AnoFabricacaoModelo = anoFabricacaoModelo;
        Cor = cor;
        UltimoLicenciamento = ultimoLicenciamento;
    }
    
    public int Id { get; private set; }
    public string Marca { get; private set; } = "";
    public string Modelo { get; private set; } = "";
    public string Placa { get; private set; } = "";
    public string NumeroRenavam { get; private set; } = "";
    public ETipoVeiculo TipoVeiculo { get; private set; }
    public ETipoCombustivel TipoCombustivel { get; private set; }
    public string AnoFabricacaoModelo { get; private set; } = "";
    public string? Cor { get; private set; }
    public string UltimoLicenciamento { get; private set; } = "";
    

    public void AlteraNumeroRenavam(string numeroRenavam) => this.NumeroRenavam = numeroRenavam;

    public void AlteraCor(string cor) => this.Cor = cor;

    public void AlteraPlaca(string placa) => this.Placa = placa;

    public void AlteraModelo(string modelo) => this.Modelo = modelo;
}