using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebRentas.Models
{
    public class AdministradorError
    {
        public static void LogError(
            string mensajeParaLog,
            string funcion,
            Exception ex,
            HttpBrowserCapabilitiesBase browser)
        {
            List<string> cadenaErrores = new List<string>();

            cadenaErrores.Add(mensajeParaLog);

            cadenaErrores.Add("");
            cadenaErrores.Add("");
            cadenaErrores.Add("*******************************************");
            cadenaErrores.Add("TRAZA GENERADA POR INFT");
            cadenaErrores.Add("*******************************************");

            long numero = 1;
            Exception exc1 = ex;
            while (exc1 != null)
            {
                if (numero > 1)
                {
                    cadenaErrores.Add("");
                    cadenaErrores.Add("");
                }

                cadenaErrores.Add("Error número " + numero.ToString("00") + ": " + exc1.Message);

                exc1 = exc1.InnerException;
                numero++;
            }

            cadenaErrores.Add("*******************************************");
            cadenaErrores.Add("FIN TRAZA GENERADA POR INFT");
            cadenaErrores.Add("*******************************************");

            cadenaErrores.Add("");
            cadenaErrores.Add("");
            cadenaErrores.Add("*******************************************");
            cadenaErrores.Add("TRAZA GENERADA POR IIS");
            cadenaErrores.Add("*******************************************");

            Exception exc2 = ex;
            numero = 1;
            while (exc2 != null)
            {
                if (numero > 1)
                {
                    cadenaErrores.Add("");
                    cadenaErrores.Add("");
                }

                cadenaErrores.Add("Error número: " + numero.ToString("00"));
                cadenaErrores.Add("Source: " + exc2.Source);
                cadenaErrores.Add("TargetSite: " + exc2.TargetSite);
                cadenaErrores.Add("StackTrace: " + exc2.StackTrace);

                exc2 = exc2.InnerException;
                numero++;
            }
            cadenaErrores.Add("*******************************************");
            cadenaErrores.Add("FIN TRAZA GENERADA POR IIS");
            cadenaErrores.Add("*******************************************");

            cadenaErrores.Add("");
            cadenaErrores.Add("");
            cadenaErrores.Add("*******************************************");
            cadenaErrores.Add("DATOS DE LA OPERACIÓN");
            cadenaErrores.Add("*******************************************");

            cadenaErrores.Add("Fecha y hora de la operación: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            cadenaErrores.Add("Nombre del servidor: " + HttpContext.Current.Server.MachineName);
            cadenaErrores.Add("*******************************************");
            cadenaErrores.Add("FIN DATOS DE LA OPERACIÓN");
            cadenaErrores.Add("*******************************************");

            if (browser != null)
            {
                cadenaErrores.Add("");
                cadenaErrores.Add("");
                cadenaErrores.Add("*******************************************");
                cadenaErrores.Add("DATOS DEL NAVEGADOR");
                cadenaErrores.Add("*******************************************");
                cadenaErrores.Add("Tipo: " + browser.Type);
                cadenaErrores.Add("Nombre: " + browser.Browser);
                cadenaErrores.Add("Versión: " + browser.Version);
                cadenaErrores.Add("Número de versión principal: " + browser.MajorVersion);
                cadenaErrores.Add("Número de versión secundario: " + browser.MinorVersion);
                cadenaErrores.Add("Plataforma: " + browser.Platform);
                cadenaErrores.Add("¿Es beta?: " + (browser.Beta ? "SI" : "NO"));
                cadenaErrores.Add("¿Es un motor de búsqueda?: " + (browser.Crawler ? "SI" : "NO"));
                cadenaErrores.Add("¿Es de la marca American Online?: " + (browser.AOL ? "SI" : "NO"));
                cadenaErrores.Add("¿Es win 16?: " + (browser.Win16 ? "SI" : "NO"));
                cadenaErrores.Add("¿Es win 32?: " + (browser.Win32 ? "SI" : "NO"));
                cadenaErrores.Add("¿Admite frames?: " + (browser.Frames ? "SI" : "NO"));
                cadenaErrores.Add("¿Admite tables?: " + (browser.Tables ? "SI" : "NO"));
                cadenaErrores.Add("¿Admite cookies?: " + (browser.Cookies ? "SI" : "NO"));
                cadenaErrores.Add("¿Admite VBScript?: " + (browser.VBScript ? "SI" : "NO"));
                cadenaErrores.Add("*******************************************");
                cadenaErrores.Add("FIN DATOS DEL NAVEGADOR");
                cadenaErrores.Add("*******************************************");
            }

            Logger.Log(string.Join("\n", cadenaErrores.ToArray()), funcion, "", "");
        }
    }
}