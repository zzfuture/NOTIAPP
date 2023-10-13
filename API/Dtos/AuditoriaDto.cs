using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class AuditoriaDto : BaseDto
    {
        public string NombreUsuario { get; set; }
        public int DescAccion { get; set; }
    }
}