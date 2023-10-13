using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class GenericoVsSubModuloConfiguration : IEntityTypeConfiguration<GenericoVsSubModulo>
    {
        public void Configure(EntityTypeBuilder<GenericoVsSubModulo> builder)
        {
            builder.ToTable("genericovssubmodulo");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id);
            builder.HasOne(x => x.PermisosGenericos).WithMany(x => x.GenericoVsSubModulos).HasForeignKey(x => x.IdGenericos);
            builder.HasOne(x => x.MaestroVsSubModulos).WithMany(x => x.GenericoVsSubModulos).HasForeignKey(x => x.IdSubmodulos);
            builder.HasOne(x => x.Roles).WithMany(x => x.GenericoVsSubModulos).HasForeignKey(x => x.IdRol);
            builder.Property(x => x.FechaCreacion).HasColumnType("date");
            builder.Property(x => x.FechaModificacion).HasColumnType("date");
        }
    }
}