using DebitosTributarios.Domain.DTOs;
using DebitosTributarios.Domain.Entities;
using DebitosTributarios.Domain.Interfaces;
using DebitosTributarios.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DebitosTributarios.Infrastructure.Repositories;

public class ContribuinteRepository(DebitosTributariosDbContext dbContext) : IContribuinteRepository
{
    public async Task NovoContribuinte(ContribuinteNovoDto contribuinte)
    {
        await dbContext.AddAsync(contribuinte);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> ContribuinteExiste(string cpf)
    {
        return await dbContext.Contribuintes.AnyAsync(c => c.CpfCnpj == cpf);
    }
}
