using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Radicado : BaseModel
    {
        public ICollection<ModuloNotificacion> ModuloNotificaciones { get; set; }
    }
}