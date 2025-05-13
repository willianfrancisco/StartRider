using Domain.Entities;
using Domain.Enums;

namespace StartRiderTests.Core.Domain.Entities;

public class LocatarioTest
{
    [Fact]
    public void AtualizaFotoCnhSucesso()
    {
        //Arrange
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
        //Act
        locatario.AtualizaFotoCnh("base64_2");

        //Assert
        Assert.Equal("base64_2", locatario.FotoCnh);
    } 
}