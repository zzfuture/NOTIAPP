using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly NotiApiContext _context;
        private IAuditoria _auditorias;
        private IBlockChain _blockchains;

        public UnitOfWork(NotiApiContext context)
        {
        _context = context;
            _context = context;
        }

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
        public IBlockChain Blockchains
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
        public IHiloRespuestaNoficacion HiloRespuestaNoficaciones
        {
            get
            {
                if (_hiloRespuestanoficaciones == null)
                {
                    _hiloRespuestanoficaciones = new HiloRespuestaNoficacionRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _hiloRespuestanoficaciones;
            }
        }
        public IMaestroVsModulo MaestrosVsModulos
        {
            get
            {
                if (_maestrosvsmodulos == null)
                {
                    _maestrosvsmodulos = new MaestroIMaestroVsModuloRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _maestrosvsmodulos;
            }
        }
        public IModuloMaestro MaestrosVsModulos
        {
            get
            {
                if (_maestrosvsmodulos == null)
                {
                    _maestrosvsmodulos = new MaestrosVsModulosRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _maestrosvsmodulos;
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
        public IRadicado Radicado
        {
            get
            {
                if (_radicado == null)
                {
                    _radicado = new RadicadoIRadicadoRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _radicado;
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
        public IRolVsMaestro Entities
        {
            get
            {
                if (_Entities == null)
                {
                    _Entities = new RolVsMaestroRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _Entities;
            }
        }
    }
}