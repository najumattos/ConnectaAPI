using Xunit;

namespace Connectamente.Tests;

public class InicializacaoTests
{
    [Fact]
    public void Teste_Deve_Passar_Para_Validar_O_Pipeline()
    {
        // Arrange (Preparação)
        var valorEsperado = true;

        // Act (Ação)
        var valorAtual = true;

        // Assert (Verificação)
        Assert.Equal(valorEsperado, valorAtual);
    }
}
