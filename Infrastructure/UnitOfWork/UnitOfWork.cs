using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private NotiApiContext _context;
        private IAuditoria _auditorias;
        private IBlockChain _blockchains;
        private IEstadoNotificacion _estadosnotificaciones;
        private IFormato _formatos;
        private IGenericoVsSubModulo _genericosvssubmodulos;
        private IHiloRespuestaNotificacion _hilorespuestanoficaciones;
        private IMaestroVsSubModulo _maestrosvssubmodulos;
        private IModuloMaestro _modulosmaestros;
        private IModuloNotificacion _modulosnotificaciones;
        private IPermisoGenerico _permisosgenericos;
        private IRadicado _radicados;
        private IRol _roles;
        private IRolVsMaestro _rolesvsmaestros;
        private ISubModulo _submodulos;
        private ITipoNotificacion _tiponotificaciones;
        private ITipoRequerimiento _tiporequerimientos;

        public IAuditoria Auditorias
        {
            get
            {
                if (_auditorias == null)
                {
                    _auditorias = new AuditoriaRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _auditorias;
            }
        }
        public IBlockChain BlockChains
        {
            get
            {
                if (_blockchains == null)
                {
                    _blockchains = new BlockChainRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _blockchains;
            }
        }
        public IEstadoNotificacion EstadosNotificaciones
        {
            get
            {
                if (_estadosnotificaciones == null)
                {
                    _estadosnotificaciones = new EstadoNotificacionRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _estadosnotificaciones;
            }
        }
        public IFormato Formatos
        {
            get
            {
                if (_formatos == null)
                {
                    _formatos = new FormatoRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _formatos;
            }
        }
        public IGenericoVsSubModulo GenericosVsSubModulos
        {
            get
            {
                if (_genericosvssubmodulos == null)
                {
                    _genericosvssubmodulos = new GenericoVsIGenericoVsSubModuloRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _genericosvssubmodulos;
            }
        }
        public IHiloRespuestaNotificacion HiloRespuestaNoficaciones
        {
            get
            {
                if (_hilorespuestanoficaciones == null)
                {
                    _hilorespuestanoficaciones = new HiloRespuestaNotificacionRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _hilorespuestanoficaciones;
            }
        }
        public IMaestroVsSubModulo MaestrosVsSubModulos
        {
            get
            {
                if (_maestrosvssubmodulos == null)
                {
                    _maestrosvssubmodulos = new MaestroVsSubModuloRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _maestrosvssubmodulos;
            }
        }
        public IModuloMaestro ModulosMaestros
        {
            get
            {
                if (_modulosmaestros == null)
                {
                    _modulosmaestros = new ModuloMaestroRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _modulosmaestros;
            }
        }
        public IModuloNotificacion ModulosNotificaciones
        {
            get
            {
                if (_modulosnotificaciones == null)
                {
                    _modulosnotificaciones = new ModuloNotificacionRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _modulosnotificaciones;
            }
        }
        public IPermisoGenerico PermisosGenericos
        {
            get
            {
                if (_permisosgenericos == null)
                {
                    _permisosgenericos = new PermisoGenericoRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _permisosgenericos;
            }
        }
        public IRadicado Radicados
        {
            get
            {
                if (_radicados == null)
                {
                    _radicados = new RadicadoRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _radicados;
            }
        }
        public IRol Roles
        {
            get
            {
                if (_roles == null)
                {
                    _roles = new RolRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _roles;
            }
        }
        public IRolVsMaestro RolesVsMaestros
        {
            get
            {
                if (_rolesvsmaestros == null)
                {
                    _rolesvsmaestros = new RolVsMaestroRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _rolesvsmaestros;
            }
        }
        public ISubModulo SubModulos
        {
            get
            {
                if (_submodulos == null)
                {
                    _submodulos = new SubModuloRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _submodulos;
            }
        }
        public ITipoNotificacion TipoNotificaciones
        {
            get
            {
                if (_tiponotificaciones == null)
                {
                    _tiponotificaciones = new TipoNotificacionRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _tiponotificaciones;
            }
        }
        public ITipoRequerimiento TipoRequerimientos
        {
            get
            {
                if (_tiporequerimientos == null)
                {
                    _tiporequerimientos = new TipoRequerimientoRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _tiporequerimientos;
            }
        }

        public UnitOfWork(NotiApiContext context)
        {
        _context = context;
            _context = context;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}