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
    public class RolRepository : GenericRepository<Rol>, IRol
    {
        private readonly NotiApiContext _context;

        public RolRepository(NotiApiContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<Rol>> GetAllAsync()
        {
            return await _context.Roles
            .Include(x => x.GenericoVsSubModulos)
            .Include(x => x.RolVsMaestros)
            .ToListAsync();
        }
        public override async Task<(int totalRegistros, IEnumerable<Rol> registros)> GetAllAsync( //Sobrecarga de metodos
            int pageIndex,
            int pageSize,
            string search
            )
            {
                var query = _context.Roles as IQueryable<Rol>;
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Nombre.ToLower().Contains(search));
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