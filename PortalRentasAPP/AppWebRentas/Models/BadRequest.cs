

using System.Web.Mvc;

namespace AppWebRentas.Models
{
    public class BadRequest
    {
        public int StatusCode { get; set; }
        public RedirectToRouteResult vista { get; set; }
        public ViewResult vistaResultado { get; set; }
    }
}