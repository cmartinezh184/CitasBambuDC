using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CitasBambuDC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Views/Citas/SignIn_Up.cshtml");
        }

    }
}