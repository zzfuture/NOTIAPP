using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class RolVsMaestroDto : BaseDto
    {
        public int IdRol { get; set; }
        public int IdMaestro { get; set; }
    }
}