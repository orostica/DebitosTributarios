namespace DebitosTributarios.Domain.Entities;

public sealed class Debito
{
    public int Id { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime DataVencimento { get; private set; }
    public DateTime? DataPagamento { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public int ContribuinteId { get; private set; }

    private Debito() { }

    //Verificacoes basicas de vencimento e pagamento
    public bool EstaVencido => DataPagamento is null && DataVencimento < DateTime.UtcNow;
    public bool EstaAberto => DataPagamento is null;


    public static Debito Criar(decimal valor, DateTime dataVencimento, int contribuinteId)
    {
        if (valor <= 0)
            throw new ArgumentException("Valor deve ser maior que zero.", nameof(valor));

        if (dataVencimento == default)
            throw new ArgumentException("Data de vencimento obrigatória.", nameof(dataVencimento));

        return new Debito
        {
            ContribuinteId = contribuinteId,
            Valor = valor,
            DataVencimento = dataVencimento,
            DataPagamento = null,
            DataCriacao = DateTime.UtcNow
        };
    }
}
