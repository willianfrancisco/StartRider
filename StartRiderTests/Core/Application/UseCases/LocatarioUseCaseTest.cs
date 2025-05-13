using Application.DTOs;
using Application.UserCases;
using Domain.Entities;
using Domain.Enums;
using Domain.Ports;
using Moq;

namespace StartRiderTests.Core.Application.UseCases;

public class LocatarioUseCaseTest
{
    [Fact]
    public async Task CadastrarNovoLocatarioSucesso()
    {
        //Arrange
        var mockLocatarioRepository = new Mock<ILocatarioRepositoryPort>();
        var mockLogger = new Mock<ISerilogLoggerService>();
        var novoLocatario = new NovoLocatarioDto(
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
        
        var useCase = new LocatarioUseCase(mockLocatarioRepository.Object, mockLogger.Object);
        //Act
        await useCase.CadastrarNovoLocatarioAsync(novoLocatario);
        //Assert
        mockLocatarioRepository.Verify(l => l.CadastrarNovoLocatarioAsync(It.IsAny<Locatario>()), Times.Once);
    }

    [Fact]
    public async Task AtualizaFotoCnhLocatarioSucesso()
    {
        //Arrange
        var mockLocatarioRepository = new Mock<ILocatarioRepositoryPort>();
        var mockLogger = new Mock<ISerilogLoggerService>();
        var locatario = new Locatario(
            1,
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

        var novaFoto = new AtualizaFotoCnhLocatarioDto("Base 64_2");
        
        mockLocatarioRepository.Setup(l => l.RecuperaLocatarioPorIdAsync(1)).ReturnsAsync(locatario);
        var useCase = new LocatarioUseCase(mockLocatarioRepository.Object, mockLogger.Object);
        
        //Act
        await useCase.AtualizaFotoCnhLocatarioAsync(1, novaFoto);
        //Assert
        mockLocatarioRepository.Verify(l => l.AtualizaFotoCnhLocatarioAsync(locatario.Id, novaFoto.NovaFotoCnh, locatario), Times.Once);
    }
}