using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class NotiApiContext : DbContext
    {
        public DbSet<Auditoria> Auditorias { get; set;}
        public DbSet<Blockchain> Blockchains { get; set;}
        public DbSet<EstadoNotificacion> EstadoNotificaciones { get; set;}
        public DbSet<Formato> Formatos { get; set;}
        public DbSet<GenericoVsSubModulo> GenericoVsSubModulos { get; set;}
        public DbSet<HiloRespuestaNotificacion> HiloRespuestaNotificaciones { get; set;}
        public DbSet<MaestroVsSubModulo> MaestroVsSubModulos { get; set;}
        public DbSet<ModuloMaestro> ModuloMaestros { get; set;}
        public DbSet<ModuloNotificacion> ModuloNotificaciones { get; set;}
        public DbSet<PermisoGenerico> PermisoGenericos { get; set;}
        public DbSet<Radicado> Radicados { get; set;}
        public DbSet<Rol> Roles { get; set;}
        public DbSet<RolVsMaestro> RolVsMaestros { get; set;}
        public DbSet<SubModulo> SubModulos { get; set;}
        public DbSet<TipoNotificacion> TipoNotificaciones { get; set;}
        public DbSet<TipoRequerimiento> TipoRequerimientos { get; set;}

    }
}