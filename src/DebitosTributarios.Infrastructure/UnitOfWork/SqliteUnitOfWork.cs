using DebitosTributarios.Infrastructure.Contexts;

namespace DebitosTributarios.Infrastructure.UnitOfWork;

internal sealed class SqliteUnitOfWork(DebitosTributariosDbContext dbContext)
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
