using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class SubModulo : BaseModel
    {
        public string NombreSubmodulo { get; set; }
        public ICollection<MaestroVsSubModulo> MaestroVsSubModulos { get; set; }
    }
}