using System;

namespace Reportes.DTOs
{
    public class NotificacionDeudaDTO
    {
        public int? IdNotificado { get; set; }
        public DateTime FechaNotificacion { get; set; }
        public int? NroNotificacion { get; set; }
        public string ApellidoNombre { get; set; }
        public int? IdCuenta { get; set; }
        public string TyC { get; set; }
        public decimal ImporteNeto { get; set; }
        public decimal ImporteTotal { get; set; }
        public decimal Interes { get; set; }
        public DateTime FechaAlcance { get; set; }
        public string Obs { get; set; }
        public bool? Entregado { get; set; }
    }
}
