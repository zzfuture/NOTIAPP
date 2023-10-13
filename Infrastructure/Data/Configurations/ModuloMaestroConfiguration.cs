using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ModuloMaestroConfiguration : IEntityTypeConfiguration<ModuloMaestro>
    {
        public void Configure(EntityTypeBuilder<ModuloMaestro> builder)
        {
            builder.ToTable("modulomaestro");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id);
            builder.Property(x => x.NombreModulo).HasMaxLength(50);
            builder.Property(x => x.FechaCreacion).HasColumnType("date");
            builder.Property(x => x.FechaModificacion).HasColumnType("date");
        }
    }
}