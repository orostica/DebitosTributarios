using DebitosTributarios.Domain.Entities;
using DebitosTributarios.Domain.Interfaces;
using DebitosTributarios.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DebitosTributarios.Infrastructure.Repositories;

public class ContribuinteRepository(DebitosTributariosDbContext dbContext) : IContribuinteRepository
{
    public async Task<Contribuinte> NovoContribuinteAsync(Contribuinte contribuinte)
    {
        // Adicionar a entidade nao o dto e retornar a entidade criada
        await dbContext.Contribuintes.AddAsync(contribuinte);
        await dbContext.SaveChangesAsync();
        return contribuinte;
    }

    public Task<bool> CpfCnpjExisteAsync(string cpfCnpj)
        => dbContext.Contribuintes.AnyAsync(c => c.CpfCnpj == cpfCnpj);

    public Task<Contribuinte?> ObterPorIdAsync(int id)
        => dbContext.Contribuintes.FirstOrDefaultAsync(c => c.Id == id);
}
