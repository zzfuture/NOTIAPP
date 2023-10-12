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
    public class GenericoVsIGenericoVsSubModuloRepository : GenericRepository<GenericoVsSubModulo>, IGenericoVsSubModulo
    {
        private readonly NotiApiContext _context;

        public GenericoVsIGenericoVsSubModuloRepository(NotiApiContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<GenericoVsSubModulo>> GetAllAsync()
        {
            return await _context.GenericoVsSubModulos.ToListAsync();
        }
        public override async Task<(int totalRegistros, IEnumerable<GenericoVsSubModulo> registros)> GetAllAsync( //Sobrecarga de metodos
            int pageIndex,
            int pageSize,
            string search
            )
            {
                var query = _context.GenericoVsSubModulos as IQueryable<GenericoVsSubModulo>;
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.IdGenericos.ToString().Contains(search));
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