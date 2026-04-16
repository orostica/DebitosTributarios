namespace DebitosTributarios.Domain.Entities;

public sealed class Contribuinte
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string CpfCnpj { get; set; }
    public DateTime DataCriacao { get; set; }

    private Contribuinte() { }

    public static Contribuinte Criar(string nome, string cpfCnpj)
    {
        return new Contribuinte
        {
            Nome = nome,
            CpfCnpj = cpfCnpj,
            DataCriacao = DateTime.UtcNow
        };
    }

}
