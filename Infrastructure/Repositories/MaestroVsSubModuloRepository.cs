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
    public class MaestroVsSubModuloRepository : GenericRepository<MaestroVsSubModulo>, IMaestroVsSubModulo
    {
        private readonly NotiApiContext _context;

        public MaestroVsSubModuloRepository(NotiApiContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<MaestroVsSubModulo>> GetAllAsync()
        {
            return await _context.MaestroVsSubModulos
            .Include(x => x.GenericoVsSubModulos)
            .ToListAsync();
        }
        public override async Task<(int totalRegistros, IEnumerable<MaestroVsSubModulo> registros)> GetAllAsync( //Sobrecarga de metodos
            int pageIndex,
            int pageSize,
            string search
            )
            {
                var query = _context.MaestroVsSubModulos as IQueryable<MaestroVsSubModulo>;
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.IdMaestro.ToString().Contains(search));
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