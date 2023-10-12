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
    public class PermisoGenericoRepository : GenericRepository<PermisoGenerico>, IPermisoGenerico
    {
        private readonly NotiApiContext _context;

        public PermisoGenericoRepository(NotiApiContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<PermisoGenerico>> GetAllAsync()
        {
            return await _context.PermisoGenericos.ToListAsync();
        }
        public override async Task<(int totalRegistros, IEnumerable<PermisoGenerico> registros)> GetAllAsync( //Sobrecarga de metodos
            int pageIndex,
            int pageSize,
            string search
            )
            {
                var query = _context.PermisoGenericos as IQueryable<PermisoGenerico>;
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.NombrePermiso.ToLower().Contains(search));
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