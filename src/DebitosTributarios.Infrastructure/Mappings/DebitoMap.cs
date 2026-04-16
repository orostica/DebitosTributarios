using DebitosTributarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DebitosTributarios.Infrastructure.Mappings;

internal sealed class DebitoMap : IEntityTypeConfiguration<Debito>
{
    public void Configure(EntityTypeBuilder<Debito> builder)
    {
        builder.ToTable("Debitos");

    }
}
