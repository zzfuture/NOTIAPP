using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Models;

namespace Infrastructure.Data.Configurations
{
    public class MaestroVsSubModuloConfiguration : IEntityTypeConfiguration<MaestroVsSubModulo>
    {
        public void Configure(EntityTypeBuilder<MaestroVsSubModulo> builder)
        {
            builder.ToTable("maestrovssubmodulo");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id);
            builder.HasOne(x => x.ModuloMaestros).WithMany(x => x.MaestroVsSubModulos).HasForeignKey(x => x.IdMaestro);
            builder.HasOne(x => x.SubModulos).WithMany(x => x.MaestroVsSubModulos).HasForeignKey(x => x.IdSubmodulo);
            builder.Property(x => x.FechaCreacion).HasColumnType("date");
            builder.Property(x => x.FechaModificacion).HasColumnType("date");
        }
    }
}