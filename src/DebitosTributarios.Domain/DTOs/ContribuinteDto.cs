namespace DebitosTributarios.Domain.DTOs
{
    public record ContribuinteDto(
        int Id,
        string Nome,
        int TotalDebitos,
        decimal TotalEmAberto,
        int QuantidadeDebitosVencidos,
        decimal TotalVencido
    );
}
