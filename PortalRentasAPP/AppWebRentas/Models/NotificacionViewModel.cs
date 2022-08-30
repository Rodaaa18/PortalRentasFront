using System;

namespace AppWebRentas.Models
{
    public class NotificacionViewModel
    {
        public int? IdNotificado { get; set; }
        public DateTime? FechaNotificacion { get; set; }
        public int? NroNotificacion { get; set; }
        public string ApellidoNombre { get; set; }
        public int? IdCuenta { get; set; }
        public string TyC { get; set; }
        public DateTime? FechaAlcance { get; set; }
        public string Obs { get; set; }
        public bool? Entregado { get; set; }
        public int start { get; set; }
        public int pageSize { get; set; }
    }
}