using DebitosTributarios.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DebitosTributarios.Domain.Interfaces
{
    public interface IDebitoRepository
    {
        Task<Debito> NovoDebitoAsync(Debito debito);
        Task<IReadOnlyList<Debito>> ObterPorContribuinteIdAsync(int contribuinteId);
    }
}
