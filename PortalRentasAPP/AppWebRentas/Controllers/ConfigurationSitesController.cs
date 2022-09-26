using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace AppWebRentas.Controllers
{
    public class ConfigurationSitesController : Controller
    {
        // GET: ConfigurationSites
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Server.MapPath("~/configuracion.xml"));
            XmlNodeList nodeList = doc.SelectNodes("configuracion/mensajes/mensaje");//doc.SelectNodes("/mensaje");

            List<ModelMensaje> listaMensajes = new List<ModelMensaje>();

            foreach (XmlNode node in nodeList)
            {
                listaMensajes.Add(new ModelMensaje()
                { nombre = node.Attributes["nombre"].InnerText, texto = node.InnerText, filename = node.Attributes["filename"].InnerText });

            }

            ViewBag.Mensajes = listaMensajes;

            return View();
        }
        public class ModelMensaje
        {
            public string nombre { get; set; }
            public string texto { get; set; }
            public string filename { get; set; }
        }
    }
}