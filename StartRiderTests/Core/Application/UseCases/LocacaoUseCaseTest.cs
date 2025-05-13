using Application.DTOs;
using Application.UserCases;
using Domain.Entities;
using Domain.Enums;
using Domain.Ports;
using Moq;

namespace StartRiderTests.Core.Application.UseCases;

public class LocacaoUseCaseTest
{
    [Fact]
    public async Task RecuperaLocacaoPorIdSucesso()
    {
        //Arrange
        var mockLocacaoRepository = new Mock<ILocacaoRepositoryPort>();
        var mockVeiculoRepository = new Mock<IVeiculoRepositoryPort>();
        var mockLocatarioRepository = new Mock<ILocatarioRepositoryPort>();
        var mockLogger = new Mock<ISerilogLoggerService>();
        var locacao =  new Locacao(1,DateTime.Now, DateTime.Now,7, 1, 1);
        //Act
        mockLocacaoRepository.Setup(l => l.RecuperaLocacaoPorIdAsync(1)).ReturnsAsync(locacao);
        var useCase = new LocacaoUseCase(mockLocacaoRepository.Object, mockLocatarioRepository.Object, mockVeiculoRepository.Object, mockLogger.Object);
        await useCase.RecuperaLocacaoPorIdAsync(1);
        //Assert
        mockLocacaoRepository.Verify(l => l.RecuperaLocacaoPorIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task CadastrarNovaLocacaoSucesso()
    {
        //Arrange
        var mockLocacaoRepository = new Mock<ILocacaoRepositoryPort>();
        var mockVeiculoRepository = new Mock<IVeiculoRepositoryPort>();
        var mockLocatarioRepository = new Mock<ILocatarioRepositoryPort>();
        var mockLogger = new Mock<ISerilogLoggerService>();
        
        //Act
        var novaLocacao = new NovaLocacaoDto(DateTime.Now, 7,1,1);
        var veiculo = new Veiculo(1,"Hyundai","Tucson","0000000","0000000000", ETipoVeiculo.Automovel, ETipoCombustivel.Gasolina,"2007","Prata","2025");
        var locatario = new Locatario(
            "willian",
            "willian.teste@gmail.com",
            "00000000000",
            new DateOnly(2025,05,17),
            "000000000",
            new DateOnly(2025,05,17),
            new DateOnly(2025,05,17),
            ECategoriaCnh.A,
            "base64"
        );
        
        mockVeiculoRepository.Setup(v => v.RecuperaVeiculoPorIdAsync(novaLocacao.VeiculoId)).ReturnsAsync(veiculo);
        mockLocatarioRepository.Setup(l => l.RecuperaLocatarioPorIdAsync(novaLocacao.LocatarioId)).ReturnsAsync(locatario);
        var useCase = new LocacaoUseCase(mockLocacaoRepository.Object, mockLocatarioRepository.Object, mockVeiculoRepository.Object, mockLogger.Object);
        await useCase.CadastrarNovaLocacaoAsync(novaLocacao);
        //Assert
        mockLocacaoRepository.Verify(l => l.CadastrarNovaLocacaoAsync(It.IsAny<Locacao>()), Times.Once);
    }

    [Fact]
    public async Task AtualizaDataDevolucaoLocacaoSucesso()
    {
        //Arrange
        var mockLocacaoRepository = new Mock<ILocacaoRepositoryPort>();
        var mockVeiculoRepository = new Mock<IVeiculoRepositoryPort>();
        var mockLocatarioRepository = new Mock<ILocatarioRepositoryPort>();
        var mockLogger = new Mock<ISerilogLoggerService>();
        var locacao = new Locacao(1,DateTime.Now,DateTime.Now,7, 1, 1);
        var novaData = new AtualizaDataDevolucaoLocacaoDto(DateTime.Now.AddDays(1));

        
        //Act
        mockLocacaoRepository.Setup(l => l.RecuperaLocacaoPorIdAsync(locacao.Id)).ReturnsAsync(locacao);
        var useCase = new LocacaoUseCase(mockLocacaoRepository.Object, mockLocatarioRepository.Object, mockVeiculoRepository.Object, mockLogger.Object);
        await useCase.AtualizaDataDevolucaoLocacaoAsync(1,novaData);
        //Assert
        mockLocacaoRepository.Verify(l => l.AtualizaDataDevolucaoLocacaoAsync(novaData.DataDevolucao, locacao), Times.Once);
    }
}