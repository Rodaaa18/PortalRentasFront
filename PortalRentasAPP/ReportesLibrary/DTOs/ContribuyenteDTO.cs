using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportesLibrary.DTOs
{
    public class ContribuyenteDTO
    {
        public int IdContribuyente { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string NroDocumento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}
