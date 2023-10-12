using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Blockchain : BaseModel
    {
        public int IdNotificacion { get; set; }
        public TipoNotificacion TipoNotificaciones { get; set; }
        public int IdHiloRespuesta { get; set; }
        public HiloRespuestaNotificacion HiloRespuestaNotificaciones { get; set; }
        public int IdAuditoria { get; set; }
        public Auditoria Auditorias { get; set; }
        public string HashGenerado { get; set; }
    }
}