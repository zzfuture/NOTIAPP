using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ModuloNotificacion : BaseModel
    {
        public string AsuntoNotificacion { get; set; }
        public int IdtpoNotificacion { get; set; }
        public TipoNotificacion TipoNotificaciones { get; set; }
        public int IdRadicado { get; set; }
        public Radicado Radicados { get; set; }
        public int IdEstadoNotificiones { get; set; }
        public EstadoNotificacion EstadosNotificacion { get; set; }
        public int IdHiloRespuesta { get; set; }
        public HiloRespuestaNotificacion HiloRespuestaNotificaciones { get; set; }
        public int IdFormato { get; set; }
        public Formato Formatos { get; set; }
        public int IdRequerimiento { get; set; }
        public TipoRequerimiento TipoRequerimientos { get; set; }
        public string TextoNotificacion { get; set; }
    }
}