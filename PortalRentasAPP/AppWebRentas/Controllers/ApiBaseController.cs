using RestSharp;
using System.Configuration;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;

namespace AppWebRentas.Controllers
{
    public class ApiBaseController : Controller
    {
        public RestClient Client { get; set; }
        public RestRequest LocalRequest { get; set; }
        public string ApiUri { get; set; }


        protected override void Initialize(RequestContext requestContext)
        {
            Client = new RestClient();
            LocalRequest = new RestRequest(Method.GET);
            ApiUri = ConfigurationManager.AppSettings["ApiUri"].ToString();
            base.Initialize(requestContext);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
    
}
