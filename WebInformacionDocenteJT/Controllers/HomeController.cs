using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebInformacionDocenteJT.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult About()
        {
            ViewBag.Message = "Acerca de la aplicacion.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "DETALLES";

            return View();
        }
    }
}