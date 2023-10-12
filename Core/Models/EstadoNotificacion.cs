using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class EstadoNotificacion : BaseModel
    {
        public string NombreEstado { get; set; }
        public ICollection<ModuloNotificacion> ModuloNotificaciones { get; set; }
    }
}