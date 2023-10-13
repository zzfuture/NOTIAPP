using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class MaestroVsSubModulo : BaseModel
    {
        public int IdMaestro { get; set; }
        public ModuloMaestro ModuloMaestros { get; set; }
        public int IdSubmodulo { get; set; }
        public SubModulo SubModulos { get; set; }
        public ICollection<GenericoVsSubModulo> GenericoVsSubModulos { get; set; }
    }
}