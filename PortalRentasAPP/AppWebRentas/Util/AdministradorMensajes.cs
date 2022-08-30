using System;
using System.Web;
using System.Xml;

namespace AppWebRentas.Util
{
    public class AdministradorMensajes
    {
        public static ModeloMensaje ObtenerMensaje(string nombre)
        {
            try
            {

                XMLReader xmlReader = new XMLReader(HttpContext.Current.Server.MapPath("~/configuracion.xml"));
                XmlNode xmlNode = xmlReader.ObtenerNodoPorAtributo("/configuracion/mensajes/mensaje", "nombre", nombre);

                ModeloMensaje modeloMensaje = null;
                if (xmlNode != null)
                {
                    modeloMensaje = new ModeloMensaje
                    {
                        Mensaje = xmlNode.InnerText.Trim(),
                        UrlImagen = xmlNode.Attributes["filename"] != null ? "ImagenesMensaje/" + xmlNode.Attributes["filename"].Value : "ImagenesMensaje/error.png"
                    };
                }

                return modeloMensaje;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public class ModeloMensaje
    {
        public string Mensaje { get; set; }
        public string UrlImagen { get; set; }
    }
}