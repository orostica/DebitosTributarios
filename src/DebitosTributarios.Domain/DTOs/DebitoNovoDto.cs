namespace DebitosTributarios.Domain.DTOs
{
   public record DebitoNovoDto(int ContribuinteId, decimal Valor, DateTime DataVencimento);
}
