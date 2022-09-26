using AppWebRentas.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace AppWebRentas.Controllers
{
    public class SuperadminController : ApiBaseController
    {
        // GET: Superadmin
        public ActionResult Index(string token)
        {
            Client.BaseUrl = new Uri(ApiUri + "token");
            LocalRequest = new RestRequest(Method.GET);
            LocalRequest.AddParameter("Token", token);
            //LocalRequest.AddHeader("Authorization", $@"Bearer {token}");
            LocalRequest.RequestFormat = DataFormat.Json;

            IRestResponse<ValuesLoginResult> respuestaApis = Client.Execute<ValuesLoginResult>(LocalRequest);

            var tokenResult = respuestaApis.Data.Result;

            Session["Token"] = respuestaApis.Data.Result;

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

            if (respuestaApis.Data.Result.ToString() == token)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Shared", "Error");
            }
        }
        [HttpPost]
        public ActionResult Edit()
        {
            var token = Session["Token"].ToString();

            if (ModelState.IsValid)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(HttpContext.Server.MapPath("~/configuracion.xml"));
                XmlNodeList nodeList = doc.SelectNodes("configuracion/mensajes/mensaje");

                List<ModelMensaje> listaMensajes = new List<ModelMensaje>();

                foreach (XmlNode node in nodeList)
                {
                    if (node.InnerText != "")
                    {
                        string nombre = node.Attributes["nombre"].InnerText;
                        string valor = Request.Form["txt_" + nombre];
                        node.FirstChild.Value = valor;

                        HttpPostedFileBase fileContent = Request.Files["file_" + nombre];
                        if (fileContent != null && fileContent.ContentLength > 0)
                        {
                            var stream = fileContent.InputStream;
                            string fileName = fileContent.FileName;
                            string path = HttpContext.Server.MapPath("~/ImagenesMensaje/");
                            string urlArchivo = fileName;

                            if (!System.IO.Directory.Exists(path))
                                System.IO.Directory.CreateDirectory(path);

                            using (var fileStream = System.IO.File.Create(Path.Combine(path, fileName)))
                            {
                                stream.CopyTo(fileStream);
                            }

                            node.Attributes["filename"].Value = fileName;
                        }

                    }
                }
                doc.Save(HttpContext.Server.MapPath("~/configuracion.xml"));
               


            }
            return RedirectToAction("Index", "Superadmin", new { token = token});
        }
        public class ModelMensaje
        {
            public string nombre { get; set; }
            public string texto { get; set; }
            public string filename { get; set; }
        }
    }
}