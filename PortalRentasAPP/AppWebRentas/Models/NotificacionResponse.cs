using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebRentas.Models
{
    public class NotificacionResponse
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public List<Result> result { get; set; }
    }
    public class Result
    {
        public int idNotificado { get; set; }
        public DateTime fechaNotificacion { get; set; }
        public int nroNotificacion { get; set; }
        public string apellidoNombre { get; set; }
        public int idCuenta { get; set; }
        public string tyC { get; set; }
        public decimal importeNeto { get; set; }
        public decimal importeTotal {get;set;}    
        public decimal interes { get; set; }
        public DateTime fechaAlcance { get; set; }
        public string obs { get; set; }
        public bool entregado { get; set; }
        public string email { get; set; }
        public int totalFilas { get; set; }
    }

}