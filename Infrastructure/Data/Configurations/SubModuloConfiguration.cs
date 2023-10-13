using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class SubModuloConfiguration : IEntityTypeConfiguration<SubModulo>
    {
        public void Configure(EntityTypeBuilder<SubModulo> builder)
        {
            builder.ToTable("submodulo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property(x => x.NombreSubmodulo).HasMaxLength(50);
            builder.Property(x => x.FechaCreacion).HasColumnType("date");
            builder.Property(x => x.FechaModificacion).HasColumnType("date");
        }
    }
}