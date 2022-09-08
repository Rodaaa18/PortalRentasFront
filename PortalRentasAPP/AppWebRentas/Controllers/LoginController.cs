using AppWebRentas.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace AppWebRentas.Controllers
{
    public class LoginController : ApiBaseController
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IniciarSession(LoginViewModel model)
        {
            Client.BaseUrl = new Uri(ApiUri + "login");
            LocalRequest = new RestRequest(Method.GET);
            LocalRequest.AddParameter("usuario",model.Usuario);
            LocalRequest.AddParameter("clave", model.Password);
            LocalRequest.RequestFormat = DataFormat.Json;

            IRestResponse<ValuesLoginResult> respuestaApi = Client.Execute<ValuesLoginResult>(LocalRequest);

            
            //var modeloList = JsonConvert.DeserializeObject<ModeloRespuesta>(respuestaApi.Content);
            

            ModeloRespuesta modeloLista = JsonConvert.DeserializeObject<ModeloRespuesta>(respuestaApi.Content);




            if (modeloLista.value.result == "" || modeloLista.value.result is null)
            {

                TempData["ErrorMessage"] = $@"Usuario o contraseña inválidos! {modeloLista.value.message.Substring(12,51)}";
                
                return RedirectToAction( "Index","Login");
                
            }
            Session["Token"] = modeloLista.value.result;


            return RedirectToAction("Index", "Notificaciones");
        }
        public ActionResult LogOff()
        {
            var url = ConfigurationManager.AppSettings["AppLogin"];
            FormsAuthentication.SignOut();
            Session.Abandon();
            if (HttpContext.Session != null) HttpContext.Session["Token"] = null;
            return Redirect(url);
        }
    }
}