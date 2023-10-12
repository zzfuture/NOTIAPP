using System.IO.Compression;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class BlockchainConfiguration : IEntityTypeConfiguration<Blockchain>
{
    public void Configure(EntityTypeBuilder<Blockchain> builder)
    {
        builder.ToTable("blockchain");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);
        builder.HasOne(x => x.TipoNotificaciones).WithMany(x => x.Blockchains);
        builder.HasOne(x => x.HiloRespuestaNotificaciones).WithMany(x => x.Blockchains);
        builder.HasOne(x => x.Auditorias).WithMany(x => x.Blockchains);
    }
}