using Domain.Entities;

namespace StartRiderTests.Core.Domain.Entities;

public class LocacaoTest
{
    [Fact]
    public void TestaCalculoValorTotalLocacaoSucesso()
    {
        //Arrange
        var locacao = new Locacao(DateTime.Now,7,1,1);
        //Act
        var result = locacao.ValorTotalLocaco;
        //Assert
        Assert.Equal(210, result);
    }

    [Fact]
    public void TestaAtualizaDataDevolucaoSucesso()
    {
        //Arrange
        var locacao = new Locacao(DateTime.Now, 7, 1, 1);
        //Act
        locacao.AtualizaDataDevolucao(new DateTime(2025,05,17));
        //Assert
        Assert.Equal(new DateTime(2025,05,17), locacao.DataDevolucao);
    }

}