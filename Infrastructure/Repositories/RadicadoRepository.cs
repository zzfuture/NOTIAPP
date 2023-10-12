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
    public class RadicadoRepository : GenericRepository<Radicado>, IRadicado
    {
        private readonly NotiApiContext _context;

        public RadicadoRepository(NotiApiContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<Radicado>> GetAllAsync()
        {
            return await _context.Radicados
            .Include(x => x.ModuloNotificaciones)
            .ToListAsync();
        }
        public override async Task<(int totalRegistros, IEnumerable<Radicado> registros)> GetAllAsync( //Sobrecarga de metodos
            int pageIndex,
            int pageSize,
            string search
            )
            {
                var query = _context.Radicados as IQueryable<Radicado>;
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Id.ToString().Contains(search));
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