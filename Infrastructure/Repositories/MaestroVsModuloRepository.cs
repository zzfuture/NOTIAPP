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
    public class MaestroVsModuloRepository : GenericRepository<MaestroVsModulo>, IMaestroVsModulo
    {
        private readonly NotiApiContext _context;

        public MaestroVsModuloRepository(NotiApiContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<MaestroVsModulo>> GetAllAsync()
        {
            return await _context.MaestroVsModulos.ToListAsync();
        }
        public override async Task<(int totalRegistros, IEnumerable<MaestroVsModulo> registros)> GetAllAsync( //Sobrecarga de metodos
            int pageIndex,
            int pageSize,
            string search
            )
            {
                var query = _context.MaestroVsModulos as IQueryable<MaestroVsModulo>;
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