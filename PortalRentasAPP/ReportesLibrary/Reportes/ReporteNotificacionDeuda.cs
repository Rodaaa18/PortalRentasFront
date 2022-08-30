using Microsoft.Reporting.WebForms;
using ReportesLibrary.DTOs;
using System;
using System.Collections.Generic;

namespace ReportesLibrary.Reportes
{
    public class ReporteNotificacionDeuda
    {
        public static ReportViewer GetRepot(DateTime FechaNotificacion, int NroNotificacion, string ApellidoNombre, long idCuenta, string TyC, DateTime FechaAlcance, string Obs, bool Entregado)
        {

            List<ModeloreporteNotificacionDeuda> Det = new List<ModeloreporteNotificacionDeuda>();

            
            ModeloreporteNotificacionDeuda modelo = new ModeloreporteNotificacionDeuda();
            

            modelo.FechaNotificacion = FechaNotificacion.ToShortDateString();
            modelo.NroNotificacion = NroNotificacion.ToString();
            modelo.ApellidoNombre = ApellidoNombre;
            modelo.IdCuenta = idCuenta.ToString();
            modelo.TyC = TyC;
            modelo.FechaAlcance = FechaAlcance.ToShortDateString();
            modelo.Obs = Obs;
            modelo.Entregado = Entregado.ToString();

            Det.Add(modelo);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.LocalReport.ReportPath = "Reportes/Reportes/ReporteNotificacionDeuda.rdlc";

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ModeloreporteNotificacionDeuda", Det));

            reportViewer.LocalReport.Refresh();
            return reportViewer;
        }
    }
}
