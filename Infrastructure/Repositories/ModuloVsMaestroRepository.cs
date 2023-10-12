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
    public class ModuloMaestroRepository : GenericRepository<ModuloMaestro>, IModuloMaestro
    {
        private readonly NotiApiContext _context;

        public ModuloMaestroRepository(NotiApiContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<ModuloMaestro>> GetAllAsync()
        {
            return await _context.ModuloMaestros.ToListAsync();
        }
        public override async Task<(int totalRegistros, IEnumerable<ModuloMaestro> registros)> GetAllAsync( //Sobrecarga de metodos
            int pageIndex,
            int pageSize,
            string search
            )
            {
                var query = _context.ModuloMaestros as IQueryable<ModuloMaestro>;
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.NombreModulo.ToLower().Contains(search));
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