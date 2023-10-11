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
        public int IdRadicado { get; set; }
        public int IdEstadoNotificiones { get; set; }
        public int IdHiloRespuesta { get; set; }
        public int IdFormato { get; set; }
        public int IdRequerimiento { get; set; }
        public string TextoNotificacion { get; set; }
        public DateOnly FechaCreacion { get; set; }
        public DateOnly FechaModificacion { get; set; }
    }
}