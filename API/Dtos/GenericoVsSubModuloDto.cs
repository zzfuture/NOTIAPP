using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class GenericoVsSubModuloDto : BaseDto
    {
        public int IdGenericos { get; set; }
        public int IdSubmodulos { get; set; }
        public int IdRol { get; set; }
    }
}