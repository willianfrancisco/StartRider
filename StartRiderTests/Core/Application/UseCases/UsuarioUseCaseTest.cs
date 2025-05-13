using Application.DTOs;
using Application.UserCases;
using Domain.Entities;
using Domain.Ports;
using Moq;

namespace StartRiderTests.Core.Application.UseCases;

public class UsuarioUseCaseTest
{
    [Fact]
    public async Task RecuperaUsuarioPorEmailSucesso()
    {
        //Arrange
        var mockUsuarioRepository = new Mock<IUsuarioRepositoryPort>();
        var mockLogger = new Mock<ISerilogLoggerService>();
        var usuario = new Usuario("willian.sousa@gmail.com","base64",new string[1]{"Admin"});
        mockUsuarioRepository.Setup(u => u.RecuperaUsuarioPorEmailAsync("willian.sousa@gmail.com")).ReturnsAsync(usuario);
        var useCase = new UsuarioUseCase(mockUsuarioRepository.Object, mockLogger.Object);
        //Act
        await useCase.RecuperaUsuarioPorEmailAsync(usuario.Email);
        //Assert
        mockUsuarioRepository.Verify(u => u.RecuperaUsuarioPorEmailAsync("willian.sousa@gmail.com"),  Times.Once);
    }

    [Fact]
    public async Task CadastraNovoUsuarioSucesso()
    {
        //Arrange
        var mockUsuarioRepository = new Mock<IUsuarioRepositoryPort>();
        var mockLogger = new Mock<ISerilogLoggerService>();
        var novoUsuario = new NovoUsuarioDto("willian.sousa@gmail.com", "base64", new string[1]{"Admin"});
        var useCase = new UsuarioUseCase(mockUsuarioRepository.Object, mockLogger.Object);
        //Act
        await useCase.CadastraNovoUsuarioAsync(novoUsuario);
        //Assert
        mockUsuarioRepository.Verify(u => u.CadastraNovoUsuarioAsync(It.IsAny<Usuario>()),  Times.Once);
    }
}