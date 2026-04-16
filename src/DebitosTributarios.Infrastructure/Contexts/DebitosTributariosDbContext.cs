using DebitosTributarios.Domain.Entities;
using DebitosTributarios.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace DebitosTributarios.Infrastructure.Contexts;

public class DebitosTributariosDbContext(DbContextOptions<DebitosTributariosDbContext> options) : DbContext(options)
{
    public DbSet<Contribuinte> Contribuintes => Set<Contribuinte>();

    public DbSet<Debito> Debitos => Set<Debito>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ContribuinteMap());
        modelBuilder.ApplyConfiguration(new DebitoMap());
    }
}
