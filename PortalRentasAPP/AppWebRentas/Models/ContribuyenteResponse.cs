using System.Collections.Generic;


namespace AppWebRentas.Models
{
    public class ContribuyenteResponse
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public List<ResultContribuyente> result { get; set; }
    }
    public class ResultContribuyente
    {
        public int IdContribuyente { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string NroDocumento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}