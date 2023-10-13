using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ModuloNotificacionConfiguration : IEntityTypeConfiguration<ModuloNotificacion>
    {
        public void Configure(EntityTypeBuilder<ModuloNotificacion> builder)
        {
            builder.ToTable("modulonotificacion");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id);
            builder.Property(x => x.AsuntoNotificacion).HasMaxLength(50);
            builder.Property(x => x.TextoNotificacion).HasMaxLength(50);
            
            builder.HasOne(x => x.TipoNotificaciones).WithMany(x => x.ModuloNotificaciones).HasForeignKey(x => x.IdtpoNotificacion);
            builder.HasOne(x => x.Radicados).WithMany(x => x.ModuloNotificaciones).HasForeignKey(x => x.IdRadicado);
            builder.HasOne(x => x.EstadosNotificacion).WithMany(x => x.ModuloNotificaciones).HasForeignKey(x => x.IdEstadoNotificiones);
            builder.HasOne(x => x.HiloRespuestaNotificaciones).WithMany(x => x.ModuloNotificaciones).HasForeignKey(x => x.IdHiloRespuesta);
            builder.HasOne(x => x.Formatos).WithMany(x => x.ModuloNotificaciones).HasForeignKey(x => x.IdFormato);
            builder.HasOne(x => x.TipoRequerimientos).WithMany(x => x.ModuloNotificaciones).HasForeignKey(x => x.IdRequerimiento);

            builder.Property(x => x.FechaCreacion).HasColumnType("date");
            builder.Property(x => x.FechaModificacion).HasColumnType("date");
        }
    }
}