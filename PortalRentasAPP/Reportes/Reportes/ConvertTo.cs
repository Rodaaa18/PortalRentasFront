using Microsoft.Reporting.WebForms;
using System;

namespace Reportes.Reportes
{
    public class ConvertTo
    {
        public static byte[] GetReportToPdf(ReportViewer rp)
        {
            byte[] pdfContent = rp.LocalReport.Render("PDF", null, out string mimeType, out string encoding, out string fileNameExtension, out string[] streams, out Warning[] warnings);

            return pdfContent;
        }

        public static byte[] GetReportToExcell(ReportViewer rp)
        {
            try
            {
                byte[] xlsContent = rp.LocalReport.Render("Excel", null, out string mimeType, out string encoding, out string fileNameExtension, out string[] streams, out Warning[] warnings);

                return xlsContent;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }        
    }
}
