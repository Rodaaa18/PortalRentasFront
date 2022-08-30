using System.Web.Mvc;

namespace AppWebRentas.Controllers
{
    public class MainMenuController : Controller
    {
        // GET: MainMenu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MostrarGrilla()
        {
            return PartialView("_GrillaParcial");
        }
    }
}