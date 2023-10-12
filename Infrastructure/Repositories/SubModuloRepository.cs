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
    public class SubModuloRepository : GenericRepository<SubModulo>, ISubModulo
    {
        private readonly NotiApiContext _context;

        public SubModuloRepository(NotiApiContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<SubModulo>> GetAllAsync()
        {
            return await _context.SubModulos
            .Include(x => x.MaestroVsSubModulos)
            .ToListAsync();
        }
        public override async Task<(int totalRegistros, IEnumerable<SubModulo> registros)> GetAllAsync( //Sobrecarga de metodos
            int pageIndex,
            int pageSize,
            string search
            )
            {
                var query = _context.SubModulos as IQueryable<SubModulo>;
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.NombreSubmodulo.ToLower().Contains(search));
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