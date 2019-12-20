using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;

namespace CitasBambuDC.Controllers
{
    public class SignIn_Up_Controller : Controller
    {
        // GET: SignIn_Up_
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SingIn_Up(int nombre, string email, string password)
        {
            BambuWS.WSDBSoapClient WS = new BambuWS.WSDBSoapClient();
            return View();
        }
    }
}