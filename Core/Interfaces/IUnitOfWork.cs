using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IAuditoria Auditorias { get; }
        public IBlockChain BlockChains { get; }
        public IEstadoNotificacion EstadosNotificaciones { get; }
        public IFormato Formatos { get; }
        public IGenericoVsSubModulo GenericosVsSubModulos { get; }
        public IHiloRespuestaNoficacion HiloRespuestaNoficaciones { get; }
        public IMaestroVsModulo MaestrosVsModulos { get; }
        public IModuloNotificacion ModulosNotificaciones { get; }
        public IPermisoGenerico PermisosGenericos { get; }
        public IRadicado Radicados { get; }
        public IRol Roles { get; }
        public IRolVsMaestro RolesVsMaestros { get; }
        public ISubModulo SubModulos { get; }
        public ITipoNotificacion TipoNotificaciones { get; }
        public ITipoRequerimiento TipoRequerimientos { get; }
        Task<int> SaveAsync();
    }
}