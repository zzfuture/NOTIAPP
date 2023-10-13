using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class AuditoriaConfiguration : IEntityTypeConfiguration<Auditoria>
{
    public void Configure(EntityTypeBuilder<Auditoria> builder)
    {
        //Here you can configure the properties using the object 'Builder'.
        builder.ToTable("Auditoria");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id);

        builder.Property(c => c.NombreUsuario).HasMaxLength(100);
        builder.Property(c => c.DescAccion).HasColumnType("int");

        builder.Property(x => x.FechaCreacion).HasColumnType("date");
        builder.Property(x => x.FechaModificacion).HasColumnType("date");
    }
}