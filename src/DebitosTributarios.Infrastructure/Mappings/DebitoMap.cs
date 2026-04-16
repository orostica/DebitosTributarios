using DebitosTributarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DebitosTributarios.Infrastructure.Mappings;

internal sealed class DebitoMap : IEntityTypeConfiguration<Debito>
{
    public void Configure(EntityTypeBuilder<Debito> builder)
    {
        builder.ToTable("debito");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        // Chave estrangeira para o contribuinte
        builder.Property(t => t.ContribuinteId)
            .IsRequired()
            .HasColumnName("contribuinte_id");

        builder.Property(t => t.Valor)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasColumnName("valor");

        builder.Property(t => t.DataVencimento)
            .IsRequired()
            .HasColumnName("data_vencimento");

        builder.Property(t => t.DataPagamento)
            .HasColumnName("data_pagamento");

        builder.Property(t => t.DataCriacao)
            .IsRequired()
            .HasColumnName("data_criacao");

        builder.Ignore(t => t.EstaVencido);
        builder.Ignore(t => t.EstaAberto);

        // Índice para agilizar consultas por contribuinte
        builder.HasIndex(t => t.ContribuinteId)
            .HasDatabaseName("ix_debito_contribuinte_id");

        // Relacionamento com Contribuinte
        builder.HasOne<Contribuinte>()
            .WithMany()
            .HasForeignKey(t => t.ContribuinteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
