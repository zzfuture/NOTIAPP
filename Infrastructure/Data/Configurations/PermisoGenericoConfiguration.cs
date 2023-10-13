using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class PermisoGenericoConfiguration : IEntityTypeConfiguration<PermisoGenerico>
    {
        public void Configure(EntityTypeBuilder<PermisoGenerico> builder)
        {
            builder.ToTable("permisogenerico");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id);
            builder.Property(x => x.NombrePermiso).HasMaxLength(50);
            builder.Property(x => x.FechaCreacion).HasColumnType("date");
            builder.Property(x => x.FechaModificacion).HasColumnType("date");
        }
    }
}