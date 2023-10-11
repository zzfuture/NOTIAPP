using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class TipoNotificacion : BaseModel
    {
        public string NombreTipo { get; set; }
        public DateOnly FechaCreacion { get; set; }
        public DateOnly FechaModificacion { get; set; }
    }
}