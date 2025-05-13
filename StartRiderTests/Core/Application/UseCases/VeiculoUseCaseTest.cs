using Application.DTOs;
using Application.UserCases;
using Domain.Entities;
using Domain.Enums;
using Domain.Ports;
using Moq;

namespace StartRiderTests.Core.Application.UseCases;

public class VeiculoUseCaseTest
{
    [Fact]
    public async Task ListarTodosOsVeiculosCadastradosSucesso()
    {
        //Arrange
        var mockVeiculoRepository = new Mock<IVeiculoRepositoryPort>();
        var mockLogger = new Mock<ISerilogLoggerService>();
        var mockPublishService = new Mock<IPublishServicePort>();
        var veiculos = new List<Veiculo>()
        {
           new Veiculo(1, "Ford", "Ka", "0000000", "000000000000", ETipoVeiculo.Automovel, ETipoCombustivel.Flex, "2020", "Preto", "2025")
        };
        mockVeiculoRepository.Setup(v => v.RecuperaTodosOsVeiculosAsync()).ReturnsAsync(veiculos);
        var useCase = new VeiculoUseCase(mockVeiculoRepository.Object, mockLogger.Object, mockPublishService.Object);
        //Act
        await useCase.ListarTodosOsVeiculosCadastradosAsync();
        //Assert
        mockVeiculoRepository.Verify(v => v.RecuperaTodosOsVeiculosAsync(), Times.Once);
    }
    
    [Fact]
    public async Task RecuperaVeiculoPorIdSucesso()
    {
        //Arrange
        var mockVeiculoRepository = new Mock<IVeiculoRepositoryPort>();
        var mockLogger = new Mock<ISerilogLoggerService>();
        var mockPublishService = new Mock<IPublishServicePort>();
        var veiculo = new Veiculo(1, "Ford", "Ka", "0000000", "000000000000", ETipoVeiculo.Automovel,
            ETipoCombustivel.Flex, "2020", "Preto", "2025");
        mockVeiculoRepository.Setup(v => v.RecuperaVeiculoPorIdAsync(1)).ReturnsAsync(veiculo);
        var useCase = new VeiculoUseCase(mockVeiculoRepository.Object, mockLogger.Object, mockPublishService.Object);
        //Act
        await useCase.RecuperaVeiculoPorIdAsync(1);
        //Assert
        mockVeiculoRepository.Verify(v => v.RecuperaVeiculoPorIdAsync(1), Times.Once);
    }
    
    [Fact]
    public async Task RecuperaVeiculoPelaPlacaSucesso()
    {
        //Arrange
        var mockVeiculoRepository = new Mock<IVeiculoRepositoryPort>();
        var mockLogger = new Mock<ISerilogLoggerService>();
        var mockPublishService = new Mock<IPublishServicePort>();
        var veiculo = new Veiculo(1, "Ford", "Ka", "0000000", "000000000000", ETipoVeiculo.Automovel,
            ETipoCombustivel.Flex, "2020", "Preto", "2025");
        mockVeiculoRepository.Setup(v => v.RecuperaVeiculoPorPlacaAsync("0000000")).ReturnsAsync(veiculo);
        var useCase = new VeiculoUseCase(mockVeiculoRepository.Object, mockLogger.Object, mockPublishService.Object);
        //Act
        await useCase.RecuperaVeiculoPelaPlacaAsync("0000000");
        //Assert
        mockVeiculoRepository.Verify(v => v.RecuperaVeiculoPorPlacaAsync("0000000"), Times.Once);
    }
    
    [Fact]
    public async Task AtualizaDadosVeiculoSucesso()
    {
        //Arrange
        var mockVeiculoRepository = new Mock<IVeiculoRepositoryPort>();
        var mockLogger = new Mock<ISerilogLoggerService>();
        var mockPublishService = new Mock<IPublishServicePort>();
        var veiculo = new Veiculo(1, "Ford", "Ka", "0000000", "000000000000", ETipoVeiculo.Automovel,
            ETipoCombustivel.Flex, "2020", "Preto", "2025");
        var atualizacao = new AtualizaVeiculoDto("000000","00000000","Preto","T-Cross");
        
        var useCase = new VeiculoUseCase(mockVeiculoRepository.Object, mockLogger.Object, mockPublishService.Object);
        //Act
        await useCase.AtualizaDadosVeiculoAsync(1,atualizacao);
        //Assert
        mockVeiculoRepository.Verify(v => v.AtualizaDadosVeiculoAsync(1,atualizacao.Placa,atualizacao.NumeroRenavam,atualizacao.Cor,atualizacao.Modelo), Times.Once);
    }
    
    [Fact]
    public async Task CadastrarNovoVeiculoSucesso()
    {
        //Arrange
        var mockVeiculoRepository = new Mock<IVeiculoRepositoryPort>();
        var mockLogger = new Mock<ISerilogLoggerService>();
        var mockPublishService = new Mock<IPublishServicePort>();
        
        var novoVeiculo = new NovoVeiculoDto("Ford", "Ka", "0000000", "000000000000", ETipoVeiculo.Automovel,
            ETipoCombustivel.Flex, "2020", "Preto", "2025");
        
        mockVeiculoRepository.Setup(v => v.RecuperaVeiculoPorPlacaAsync("0000000")).ReturnsAsync((Veiculo)null);
        
        var useCase = new VeiculoUseCase(mockVeiculoRepository.Object, mockLogger.Object, mockPublishService.Object);
        //Act
        await useCase.CadastrarNovoVeiculoAsync(novoVeiculo);
        //Assert
        mockPublishService.Verify(p => p.PublicaMensagemRabbitMqAsync(It.IsAny<string>()), Times.Once);
    }
    
    [Fact]
    public async Task DeletarUmVeiculoCadastradoSucesso()
    {
        //Arrange
        var mockVeiculoRepository = new Mock<IVeiculoRepositoryPort>();
        var mockLogger = new Mock<ISerilogLoggerService>();
        var mockPublishService = new Mock<IPublishServicePort>();
        var veiculo = new Veiculo(1, "Ford", "Ka", "0000000", "000000000000", ETipoVeiculo.Automovel,
            ETipoCombustivel.Flex, "2020", "Preto", "2025");
        mockVeiculoRepository.Setup(v => v.RecuperaVeiculoPorIdAsync(1)).ReturnsAsync(veiculo);
        var useCase = new VeiculoUseCase(mockVeiculoRepository.Object, mockLogger.Object, mockPublishService.Object);
        //Act
        await useCase.DeletarUmVeiculoCadastradoAsync(1);
        //Assert
        mockVeiculoRepository.Verify(v => v.DeletarVeiculoCadastradoAsync(It.IsAny<Veiculo>()), Times.Once);
    }
    
}