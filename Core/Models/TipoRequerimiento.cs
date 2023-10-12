using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class TipoRequerimiento : BaseModel
    {
        public string Nombre { get; set; }
        public ICollection<ModuloNotificacion> ModuloNotificaciones { get; set; }
    }
}