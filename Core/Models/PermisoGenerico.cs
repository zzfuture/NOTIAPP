using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class PermisoGenerico : BaseModel
    {
        public string NombrePermiso { get; set; }
        public ICollection<GenericoVsSubModulo> GenericoVsSubModulos { get; set; }
    }
}