using Domain.Entities;
using Domain.Enums;

namespace StartRiderTests.Core.Domain.Entities;

public class VeiculoTest
{
    [Fact]
    public void AtualizaNumeroRenavamVeiculoSucesso()
    {
        //Arrange
        var veiculo = new Veiculo(
            1,
            "Ford",
            "Ka",
            "0000000",
            "000000000000",
            ETipoVeiculo.Automovel,
            ETipoCombustivel.Flex,
            "2020",
            "Preto",
            "2025"
            );
        //Act
        veiculo.AlteraNumeroRenavam("1111111111");
        //Assert
        Assert.Equal("1111111111", veiculo.NumeroRenavam);
    }
    
    [Fact]
    public void AtualizaNumeroCorVeiculoSucesso()
    {
        //Arrange
        var veiculo = new Veiculo(
            1,
            "Ford",
            "Ka",
            "0000000",
            "000000000000",
            ETipoVeiculo.Automovel,
            ETipoCombustivel.Flex,
            "2020",
            "Preto",
            "2025"
        );
        //Act
        veiculo.AlteraCor("Branco");
        //Assert
        Assert.Equal("Branco", veiculo.Cor);
    }
    
    [Fact]
    public void AtualizaNumeroPlacaVeiculoSucesso()
    {
        //Arrange
        var veiculo = new Veiculo(
            1,
            "Ford",
            "Ka",
            "0000000",
            "000000000000",
            ETipoVeiculo.Automovel,
            ETipoCombustivel.Flex,
            "2020",
            "Preto",
            "2025"
        );
        //Act
        veiculo.AlteraPlaca("1112233");
        //Assert
        Assert.Equal("1112233", veiculo.Placa);
    }
    
    [Fact]
    public void AtualizaNumeroModeloVeiculoSucesso()
    {
        //Arrange
        var veiculo = new Veiculo(
            1,
            "Ford",
            "Ka",
            "0000000",
            "000000000000",
            ETipoVeiculo.Automovel,
            ETipoCombustivel.Flex,
            "2020",
            "Preto",
            "2025"
        );
        //Act
        veiculo.AlteraModelo("EcoSport");
        //Assert
        Assert.Equal("EcoSport", veiculo.Modelo);
    }
}