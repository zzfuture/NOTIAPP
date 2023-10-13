using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class EstadoNotificacionConfiguration : IEntityTypeConfiguration<EstadoNotificacion>
    {
        public void Configure(EntityTypeBuilder<EstadoNotificacion> builder)
        {
            builder.ToTable("estadonotificacion");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id);
            builder.Property(x => x.NombreEstado).HasMaxLength(100);
            builder.Property(x => x.FechaCreacion).HasColumnType("date");
            builder.Property(x => x.FechaModificacion).HasColumnType("date");
        }
    }
}