using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class MaestroVsSubModuloDto : BaseDto
    {
        public int IdMaestro { get; set; }
        public int IdSubmodulo { get; set; }
    }
}