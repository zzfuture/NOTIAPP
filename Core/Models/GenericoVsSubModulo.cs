using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class GenericoVsSubModulo : BaseModel
    {
        public int IdGenericos { get; set; }
        public int IdSubmodulos { get; set; }
        public int IdRol { get; set; }
        public DateOnly FechaCreacion { get; set; }
        public DateOnly FechaModificacion { get; set; }
    }
}