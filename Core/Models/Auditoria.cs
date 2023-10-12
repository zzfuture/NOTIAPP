using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Auditoria : BaseModel
    {
        public string NombreUsuario { get; set; }
        public int DescAccion { get; set; }
        public ICollection<Blockchain> Blockchains { get; set; }
    }
}