using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class RolVsMaestroConfiguration : IEntityTypeConfiguration<RolVsMaestro>
    {
        public void Configure(EntityTypeBuilder<RolVsMaestro> builder)
        {
            builder.ToTable("rolvsmaestro");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id);
            builder.HasOne(x => x.Roles).WithMany(x => x.RolVsMaestros).HasForeignKey(x => x.IdRol);
            builder.HasOne(x => x.ModuloMaestros).WithMany(x => x.RolVsMaestros).HasForeignKey(x => x.IdMaestro);
            builder.Property(x => x.FechaCreacion).HasColumnType("date");
            builder.Property(x => x.FechaModificacion).HasColumnType("date");
        }
    }
}