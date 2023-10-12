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
    public class AuditoriaRepository : GenericRepository<Auditoria>, IAuditoria
    {
        private readonly NotiApiContext _context;

        public AuditoriaRepository(NotiApiContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<Auditoria>> GetAllAsync()
        {
            return await _context.Auditorias.ToListAsync();
        }
        public override async Task<(int totalRegistros, IEnumerable<Auditoria> registros)> GetAllAsync( //Sobrecarga de metodos
            int pageIndex,
            int pageSize,
            string search
            )
            {
                var query = _context.Auditorias as IQueryable<Auditoria>;
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.DescAccion.ToString().Contains(search));
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