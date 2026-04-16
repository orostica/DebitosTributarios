using DebitosTributarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DebitosTributarios.Infrastructure.Mappings;

internal sealed class ContribuinteMap : IEntityTypeConfiguration<Contribuinte>
{
    public void Configure(EntityTypeBuilder<Contribuinte> builder)
    {
        builder.ToTable("contribuinte");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Nome)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("nome");

        builder.Property(t => t.CpfCnpj)
            .IsRequired()
            .HasMaxLength(14)
            .HasColumnName("cpf_cnpj");

        builder.Property(t => t.DataCriacao)
            .IsRequired()
            .HasColumnName("data_criacao");

        // Índice para garantir que o cpf/cnpj seja unico na tabela
        builder.HasIndex(t => t.CpfCnpj)
            .IsUnique()
            .HasDatabaseName("ix_contribuinte_cpf_cnpj");
    }
}
