using DebitosTributarios.Domain.Entities;

namespace DebitosTributarios.Tests;

public class DebitosTestes
{
    //Testes de criacao de debito
    [Fact]
    public void Criar_DeveRetornarDebitoValido_QuandoDadosCorretos()
    {
        // Arrange
        var contribuinteId = 1;
        var valor = 150.00m;
        var vencimento = DateTime.UtcNow.AddDays(30);

        // Act
        var debito = Debito.Criar(valor, vencimento, contribuinteId);

        // Assert
        Assert.Equal(0, debito.Id);
        Assert.Equal(contribuinteId, debito.ContribuinteId);
        Assert.Equal(valor, debito.Valor);
        Assert.Equal(vencimento, debito.DataVencimento);
        Assert.Null(debito.DataPagamento);
    }

    [Fact]
    public void Criar_DeveLancarException_QuandoValorZero()
    {
        // Arrange
        var contribuinteId = 1;

        // Act & Assert — regra de negócio: valor > 0
        var ex = Assert.Throws<ArgumentException>(() =>
            Debito.Criar(0m, DateTime.UtcNow.AddDays(10), contribuinteId));

        Assert.Contains("Valor deve ser maior que zero", ex.Message);
    }

    [Fact]
    public void Criar_DeveLancarException_QuandoValorNegativo()
    {
        // Arrange
        var contribuinteId = 1;

        // Act & Assert — regra de negócio: valor > 0
        var ex = Assert.Throws<ArgumentException>(() =>
            Debito.Criar(-10m, DateTime.UtcNow.AddDays(10), contribuinteId));

        Assert.Contains("Valor deve ser maior que zero", ex.Message);
    }

    [Fact]
    public void Criar_DeveLancarException_QuandoDataVencimentoDefault()
    {
        // Arrange
        var contribuinteId = 1;

        // Act & Assert — regra de negócio: data de vencimento obrigatória
        var ex = Assert.Throws<ArgumentException>(() =>
            Debito.Criar(100m, default, contribuinteId));

        Assert.Contains("Data de vencimento obrigatória.", ex.Message);
    }
}