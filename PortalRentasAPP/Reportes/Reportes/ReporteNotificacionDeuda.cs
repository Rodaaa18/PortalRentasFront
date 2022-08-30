using Microsoft.Reporting.WebForms;
using Reportes.DTOs;

namespace Reportes.Reportes
{
    public class ReporteNotificacionDeuda
    {
        public static ReportViewer GetRepot(ContribuyenteDTO contribuyente, NotificacionDeudaDTO notificacion)
        {
            ModeloreporteNotificacionDeuda modelo = new ModeloreporteNotificacionDeuda();
            
            modelo.ApellidoNombre = contribuyente.Apellido;
            modelo.NroDocumento = contribuyente.NroDocumento;
            modelo.Telefono = contribuyente.Telefono;

            modelo.FechaNotificacion = notificacion.FechaNotificacion.ToShortDateString();
            modelo.NroNotificacion = notificacion.NroNotificacion.ToString();
            modelo.ApellidoNombre = notificacion.ApellidoNombre;
            modelo.IdCuenta = notificacion.IdCuenta.ToString();
            modelo.TyC = notificacion.TyC;
            modelo.FechaAlcance = notificacion.FechaAlcance.ToShortDateString();
            modelo.Obs = notificacion.Obs;
            modelo.Entregado = notificacion.Entregado.ToString();

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.LocalReport.ReportPath = "Reportes/Reportes/ReporteNotificacionDeuda.rdlc";

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ModeloreporteNotificacionDeuda", modelo));

            reportViewer.LocalReport.Refresh();
            return reportViewer;
        }
    }
}
