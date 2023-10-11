using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Formato : BaseModel
    {
        public string NombreFormato { get; set; }
        public DateOnly FechaCreacion { get; set; }
        public DateOnly FechaModificacion { get; set; }
    }
}