using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class HiloRespuestaNotificacion : BaseModel
    {
        public string NombreTipo { get; set; }
        public ICollection<ModuloNotificacion> ModuloNotificaciones { get; set; }
        public ICollection<Blockchain> Blockchains { get; set; }
    }
}