using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Rol : BaseModel
    {
        public string Nombre { get; set; }
        public ICollection<GenericoVsSubModulo> GenericoVsSubModulos { get; set; }
    }
}