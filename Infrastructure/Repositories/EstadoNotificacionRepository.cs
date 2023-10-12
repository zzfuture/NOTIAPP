using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EstadoNotificacionRepository : GenericRepository<EstadoNotificacion>, IEstadoNotificacion
    {
        private readonly NotiApiContext _context;

        public EstadoNotificacionRepository(NotiApiContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<EstadoNotificacion>> GetAllAsync()
        {
            return await _context.EstadoNotificaciones
                .Include(x => x.ModuloNotificaciones)
                .ToListAsync();
        }
        public override async Task<(int totalRegistros, IEnumerable<EstadoNotificacion> registros)> GetAllAsync( //Sobrecarga de metodos
            int pageIndex,
            int pageSize,
            string search
        )
        {
            var query = _context.EstadoNotificaciones as IQueryable<EstadoNotificacion>;
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.NombreEstado.ToLower().Contains(search));
            }
            query = query.OrderBy(p => p.Id);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (totalRegistros, registros);
        }
    }
}