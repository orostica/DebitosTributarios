namespace DebitosTributarios.Domain.Entities;

public sealed class Debito
{
    public int Id { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime DataVencimento { get; private set; }
    public DateTime DataPagamento { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public int ContribuinteId { get; private set; }
    public List<Contribuinte> Contribuintes { get; private set; }

    private Debito() { }

    public static Debito Criar(decimal valor, DateTime dataVencimento, int contribuinteId)
    {
        return new Debito
        {
            Valor = valor,
            DataVencimento = dataVencimento,
            ContribuinteId = contribuinteId,
            DataCriacao = DateTime.UtcNow
        };
    }
}
