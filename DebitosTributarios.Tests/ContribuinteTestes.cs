using DebitosTributarios.Domain.Entities;

namespace DebitosTributarios.Tests
{
    public class ContribuinteTestes
    {
        //Testes de contribuinte
        [Fact]
        public void Contribuinte_Criar_DeveRetornarContribuinteValido_QuandoDadosCorretos()
        {
            // Arrange
            const string nome = "Maria Souza";
            const string cpfCnpj = "12345678901";

            // Act
            var contribuinte = Contribuinte.Criar(nome, cpfCnpj);

            // Assert
            Assert.Equal(0, contribuinte.Id);
            Assert.Equal(nome, contribuinte.Nome);
            Assert.Equal(cpfCnpj, contribuinte.CpfCnpj);
        }

        [Fact]
        public void Contribuinte_Criar_DeveLancarException_QuandoNomeVazio()
        {
            // Act e Assert, nome obrigatório
            var ex = Assert.Throws<ArgumentException>(() =>
                Contribuinte.Criar(string.Empty, "12345678901"));

            Assert.Contains("Nome obrigatorio", ex.Message);
        }

        [Fact]
        public void Contribuinte_Criar_DeveLancarException_QuandoCpfCnpjVazio()
        {
            // Act e Assert, CPF/CNPJ obrigatório
            var ex = Assert.Throws<ArgumentException>(() =>
                Contribuinte.Criar("João Silva", string.Empty));

            Assert.Contains("Cpf/Cnpj obrigatorio", ex.Message);
        }
    }
}
