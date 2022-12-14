using AppWebRentas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.WebForms;
using Microsoft.ReportingServices.Interfaces;
using Newtonsoft.Json;
using ReportesLibrary.DTOs;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.Mail;
using System.Web.Mvc;
using ActionResult = System.Web.Mvc.ActionResult;

namespace AppWebRentas.Controllers
{
    public class NotificacionesController : ApiBaseController
    {
        // GET: Notificaciones
        [System.Web.Mvc.HttpGet]
        public ActionResult Index(string token)
        {
            try
            {
                Client.BaseUrl = new Uri(ApiUri + "token");
                LocalRequest = new RestRequest(Method.GET);
                LocalRequest.AddParameter("Token", token);
                //LocalRequest.AddHeader("Authorization", $@"Bearer {token}");
                LocalRequest.RequestFormat = DataFormat.Json;

                IRestResponse<ValuesLoginResult> respuestaApis = Client.Execute<ValuesLoginResult>(LocalRequest);

                var tokenResult = respuestaApis.Data.Result;

                Session["Token"] = respuestaApis.Data.Result;

                if (respuestaApis.Data.Result.ToString() == token)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Shared", "Error");
                }
            }
            catch(Exception ex)
            {
                AdministradorError.LogError("Se generó un error inesperado", "Index", ex, Request != null ? Request.Browser : null);
                return Json(Models.JsonReturn.ErrorConMsjSimple(Util.AdministradorMensajes.ObtenerMensaje("error_listar_inmuebles")), JsonRequestBehavior.AllowGet);
            }
           
            
        }
        public System.Web.Mvc.ActionResult GetTokenUser(string usuario)
        {
            Client.BaseUrl = new Uri(ApiUri + "token");
            LocalRequest = new RestRequest(Method.GET);
            LocalRequest.AddParameter("Usuario", usuario);
            //LocalRequest.AddHeader("Authorization", $@"Bearer {token}");
            LocalRequest.RequestFormat = DataFormat.Json;

            IRestResponse<ValuesLoginResult> respuestaApi = Client.Execute<ValuesLoginResult>(LocalRequest);

            NotificacionResponse Response = JsonConvert.DeserializeObject<NotificacionResponse>(respuestaApi.Content);

            return RedirectToAction("Index", "MainMenu");
        }
        public System.Web.Mvc.ActionResult GetNotificaciones(NotificacionViewModel model)
        {
            Client.BaseUrl = new Uri(ApiUri + "notificacion");
            LocalRequest = new RestRequest(Method.GET);
            LocalRequest.AddParameter("start", model.start);
            LocalRequest.AddParameter("pageSize", model.pageSize);
            //LocalRequest.AddHeader("Authorization", $@"Bearer {token}");
            LocalRequest.RequestFormat = DataFormat.Json;

            IRestResponse<ValuesLoginResult> respuestaApi = Client.Execute<ValuesLoginResult>(LocalRequest);

            NotificacionResponse Response = JsonConvert.DeserializeObject<NotificacionResponse>(respuestaApi.Content);



            Session["Notificaciones"] = Response.result;

            return RedirectToAction("Index", "MainMenu");

        }


        public JsonResult ListarNotificacionesGrillaPaginada(ModeloConsultaGrilla modeloConsulta, ModeloExtraParamNotificacion extraParam)
        {
            Client.BaseUrl = new Uri(ApiUri + "notificacion");
            LocalRequest = new RestRequest(Method.GET);
            LocalRequest.AddParameter("start", modeloConsulta.start);
            LocalRequest.AddParameter("pageSize", modeloConsulta.length);
            //LocalRequest.AddHeader("Authorization", $@"Bearer {token}");
            LocalRequest.RequestFormat = DataFormat.Json;

            IRestResponse<ValuesLoginResult> respuestaApi = Client.Execute<ValuesLoginResult>(LocalRequest);

            NotificacionResponse Response = JsonConvert.DeserializeObject<NotificacionResponse>(respuestaApi.Content);



            Session["Notificaciones"] = Response.result;



            //if (Session == null || Session["Token"] == null)
            //{
            //    return Json(Models.JsonReturn.RedireccionarIndex());
            //}
            List<Result> ListNotificaciones = (List<Result>)Session["Notificaciones"];
            List<object> listaResultado = new List<object>();
            List<Result> listaPaginada = new List<Result>();

            //Búsqueda por Letras
            string buscqueda = (modeloConsulta.search.value == null ? "" : modeloConsulta.search.value);

            if (buscqueda.Length > 0 || buscqueda != "")
            {
                var listPrimerFiltro = ListNotificaciones.Where(l => (l.apellidoNombre).ToString().Contains(buscqueda)).ToList();

                var filtroBusqueda = listPrimerFiltro.ToList();

                ListNotificaciones = filtroBusqueda;
            }
            //Orden
            int col = (int)modeloConsulta.order[0].column;
            switch (col)
            {
                case 0:
                    if (modeloConsulta.order[0].dir == ModeloDireccion.asc)
                    {
                        listaPaginada = ListNotificaciones.OrderBy(x => x.apellidoNombre)
                                                                    .Skip(modeloConsulta.start)
                                                                    .Take(modeloConsulta.length)
                                                                    .ToList();
                    }
                    else
                    {
                        listaPaginada = ListNotificaciones.OrderByDescending(x => x.apellidoNombre)
                                                                    .Skip(modeloConsulta.start)
                                                                    .Take(modeloConsulta.length)
                                                                    .ToList();
                    }
                    break;
                case 1:
                    if (modeloConsulta.order[0].dir == ModeloDireccion.asc)
                    {
                        listaPaginada = ListNotificaciones.OrderBy(x => x.nroNotificacion)
                                                                    .Skip(modeloConsulta.start)
                                                                    .Take(modeloConsulta.length)
                                                                    .ToList();
                    }
                    else
                    {
                        listaPaginada = ListNotificaciones.OrderByDescending(x => x.nroNotificacion)
                                                                    .Skip(modeloConsulta.start)
                                                                    .Take(modeloConsulta.length)
                                                                    .ToList();
                    }
                    break;
                default:
                    listaPaginada = ListNotificaciones.OrderBy(x => x.idCuenta)
                                                                    .Skip(modeloConsulta.start)
                                                                    .Take(modeloConsulta.length)
                                                                    .ToList();
                    break;
            }
            //Datos
            foreach (Result resultados in listaPaginada)
            {
                listaResultado.Add(new
                {
                    ApellidoNombre = resultados.apellidoNombre,
                    NroNotificacion = resultados.nroNotificacion,
                    FechaNotificacion = resultados.fechaNotificacion.ToShortDateString(),
                    FechaAlcance = resultados.fechaAlcance.ToShortDateString(),
                    ImporteNeto = resultados.importeNeto,
                    ImporteTotal = resultados.importeTotal,
                    Interes = resultados.interes,
                    TyC = resultados.tyC,
                    IdCuenta = resultados.idCuenta,
                    Observacion = resultados.obs,
                    Email = resultados.email,
                });
            }
            return Json(Models.JsonReturn.SuccessConRetorno(new
            {
                draw = modeloConsulta.draw,
                recordsTotal = ListNotificaciones.Select(x=> x.totalFilas).FirstOrDefault(),
                recordsFiltered = ListNotificaciones.Count(),
                data = listaResultado
            }));
        }
        [System.Web.Http.HttpPost]
        public System.Web.Mvc.ActionResult SendEmail(string idCuenta)
        {
            if (Session["Token"] is null || Session["Token"].ToString() == "")
            {
                TempData["ErrorMessage"] = $@"Debe Iniciar Sesión";

                return Json(Models.JsonReturn.RedireccionarIndex());
            }


            string pathReportes = Server.MapPath("~\\Reportes\\Reportes");

            ReportViewer rp = null;

            var notificacion = Session["Notificaciones"].ToString();

            List<Result> listaNotificaciones = (List<Result>)Session["Notificaciones"];

            var notificacionActual = listaNotificaciones.FirstOrDefault(f => f.email == idCuenta);

            var model = Client.Execute<ReportesLibrary.DTOs.ContribuyenteDTO>(LocalRequest);

            rp = ReportesLibrary.Reportes.ReporteNotificacionDeuda.GetRepot(notificacionActual.fechaNotificacion, notificacionActual.nroNotificacion, notificacionActual.apellidoNombre, notificacionActual.idCuenta, notificacionActual.tyC, notificacionActual.fechaAlcance, notificacionActual.obs, notificacionActual.entregado);

            byte[] myPDF = ReportesLibrary.Reportes.ConvertTo.GetReportToPdf(rp);



            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Type", "application/pdf");
            Response.AddHeader("Content-Disposition", "inline; filename=NotificacionDeuda_.pdf");
            Response.BinaryWrite(myPDF);
            string strFilePath = @"C:\Users\INFT\AppData\Local\Temp";
            string strFileName = "NotificacionDeuda_.pdf";
            string filename = Path.Combine(strFilePath, strFileName);
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                fs.Write(myPDF, 0, myPDF.Length);
            }
            Response.End();

            string strTempArchivo = Path.GetTempPath() + "NotificacionDeuda_.pdf";
            string strConfigNamespace = "http://schemas.microsoft.com/cdo/configuration/";


            System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage(WebConfigurationManager.AppSettings["UserName"], idCuenta);
            //email.cc.add("mailcopia@dominio.com");
            SmtpClient smtp = new SmtpClient(WebConfigurationManager.AppSettings["Host"], 587);
            Attachment Data = new Attachment(strTempArchivo, MediaTypeNames.Application.Octet);
            var html = "<h1> Titulo</h1>";
            email.Subject = "Notificación de Deuda";
            email.IsBodyHtml = false; //VER SI ES HTML O NO (Estilo y formato del email)
            email.Body = "Este es un mensaje blablabla";
            email.Attachments.Add(Data);
            smtp.Credentials = new System.Net.NetworkCredential(WebConfigurationManager.AppSettings["UserName"], WebConfigurationManager.AppSettings["Password"]);

            smtp.Host = WebConfigurationManager.AppSettings["Host"];

            smtp.Timeout = 10000; //MILISEGUNDOS
            smtp.EnableSsl = true;
            smtp.Send(email)
           ;

            return RedirectToAction("Index", "MainMenu");
        }
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
        public class ModeloExtraParamNotificacion
        {
            public List<ExtraParamNotific> Params { get; set; }
        }
        public class ExtraParamNotific
        {
            public int start { get; set; }
            public int pageSize { get; set; }
        }

    }
}
