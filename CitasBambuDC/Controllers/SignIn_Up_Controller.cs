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
            return View("~/Views/Citas/SignIn_Up.cshtml");
        }

        [HttpPost]
        public ActionResult Register(string nombre, string segundoNombre, string primerApellido,
            string segundoApellido, int cedulaPersona, string telefonoPersona,
            string email, string password)
        {
            BambuWS.WSDBSoapClient WS = new BambuWS.WSDBSoapClient();
            if(WS.CrearUsuario(cedulaPersona, nombre,segundoNombre,primerApellido, segundoApellido, telefonoPersona, email, password))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        [HttpPost]
        public ActionResult Login(string correoLogin, string passwordLogin)
        {
            BambuWS.WSDBSoapClient WS = new BambuWS.WSDBSoapClient();
            if(WS.LogIn(correoLogin, passwordLogin))
            {
                return RedirectToAction("ListAppointments", "Citas");
            }
            else
            {
                ViewData["Message"] = "Credenciales Incorrectas";
                return View("~/Views/Citas/SignIn_Up.cshtml");
            }
        }
    }
}