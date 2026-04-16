using DebitosTributarios.Domain.Entities;
using DebitosTributarios.Domain.Interfaces;
using DebitosTributarios.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DebitosTributarios.Infrastructure.Repositories;

public class DebitoRepository(DebitosTributariosDbContext dbContext) : IDebitoRepository
{
    public async Task<Debito> NovoDebitoAsync(Debito debito)
    {
        await dbContext.Debitos.AddAsync(debito);
        await dbContext.SaveChangesAsync();
        return debito;
    }

    public async Task<IReadOnlyList<Debito>> ObterPorContribuinteIdAsync(int contribuinteId)
        => await dbContext.Debitos
            .Where(d => d.ContribuinteId == contribuinteId)
            .ToListAsync();
}