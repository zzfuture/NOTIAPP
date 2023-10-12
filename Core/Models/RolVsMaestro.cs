using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class RolVsMaestro : BaseModel
    {
        public int IdRol { get; set; }
        public Rol Roles { get; set; }
        public int IdMaestro { get; set; }
        public ModuloMaestro ModuloMaestros { get; set; }

    }
}