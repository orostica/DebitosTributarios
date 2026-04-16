namespace DebitosTributarios.Domain.Entities;

public sealed class Contribuinte
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string CpfCnpj { get; private set; }
    public DateTime DataCriacao { get; private set; }

    private Contribuinte() { }

    public static Contribuinte Criar(string nome, string cpfCnpj)
    {
        //Validações básicas
        if(string.IsNullOrWhiteSpace(nome))
           throw new ArgumentException("Nome obrigatorio", nameof(nome));

        if(string.IsNullOrWhiteSpace(cpfCnpj))
           throw new ArgumentException("Cpf/Cnpj obrigatorio", nameof(cpfCnpj));

        return new Contribuinte
        {
            Nome = nome,
            CpfCnpj = cpfCnpj.Trim(),
            DataCriacao = DateTime.UtcNow
        };
    }

}
