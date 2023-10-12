using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ModuloMaestro : BaseModel
    {
        public string NombreModulo { get; set; }
        public ICollection<RolVsMaestro> RolVsMaestros { get; set; }
        public ICollection<MaestroVsSubModulo> MaestroVsSubModulos { get; set; }
    }
}