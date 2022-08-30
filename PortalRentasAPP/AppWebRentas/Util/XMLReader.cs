using System;
using System.Xml;

namespace AppWebRentas.Util
{
    public class XMLReader
    {
        private XmlDocument documento
        {
            get;
            set;
        }

        public XMLReader(string nombreArchivo)
        {
            try
            {
                documento = new XmlDocument();
                documento.Load(nombreArchivo);
            }
            catch (Exception)
            {
                return;
            }
        }

        public string LeerNodo(string nombreNodo)
        {
            try
            {
                return documento.SelectSingleNode("//" + nombreNodo).InnerText.Trim();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public XmlNode ObtenerNodoPorAtributo(string ruta, string nombreAtributo, string valorAtributo)
        {
            try
            {
                string rutaCompleta = ruta + "[@" + nombreAtributo + "='" + valorAtributo + "']";

                XmlNodeList lista = documento.SelectNodes(rutaCompleta);

                if (lista != null && lista.Count > 0) return lista[0];

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}