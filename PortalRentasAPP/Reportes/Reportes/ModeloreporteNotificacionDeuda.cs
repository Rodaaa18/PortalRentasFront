namespace Reportes.Reportes
{
    public class ModeloreporteNotificacionDeuda
    {
        /* Encabezado */
        public string IdContribuyente { get; set; }
        public string NroDocumento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        /* Cuerpo */
        public string IdNotificado { get; set; }
        public string FechaNotificacion { get; set; }
        public string NroNotificacion { get; set; }
        public string ApellidoNombre { get; set; }
        public string IdCuenta { get; set; }
        public string TyC { get; set; }
        public string FechaAlcance { get; set; }
        public string Obs { get; set; }
        public string Entregado { get; set; }
    }
}
