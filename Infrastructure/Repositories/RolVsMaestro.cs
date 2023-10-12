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
    public class RolVsMaestroRepository : GenericRepository<RolVsMaestro>, IRolVsMaestro
    {
        private readonly NotiApiContext _context;

        public RolVsMaestroRepository(NotiApiContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<RolVsMaestro>> GetAllAsync()
        {
            return await _context.RolVsMaestros.ToListAsync();
        }
        public override async Task<(int totalRegistros, IEnumerable<RolVsMaestro> registros)> GetAllAsync( //Sobrecarga de metodos
            int pageIndex,
            int pageSize,
            string search
            )
            {
                var query = _context.RolVsMaestros as IQueryable<RolVsMaestro>;
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.IdRol.ToString().Contains(search));
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