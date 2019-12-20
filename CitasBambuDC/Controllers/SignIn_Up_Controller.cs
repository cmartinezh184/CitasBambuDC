using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Web.Security;

namespace CitasBambuDC.Controllers
{
    public class SignIn_Up_Controller : Controller
    {
        // GET: SignIn_Up_
        public ActionResult Index()
        {
            Session.Contents.Remove("login");
            FormsAuthentication.SignOut();
            Session.Timeout = 1;
            bool IsPostBack = false;
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                Response.Cache.SetAllowResponseInBrowserHistory(false);
                Response.Cache.SetNoStore();
            }
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
                return RedirectToAction("SignIn_Up_", "Home");
            }
            return View();
        }


        [HttpPost]
        public ActionResult Login(string correoLogin, string passwordLogin)
        {
            BambuWS.WSDBSoapClient WS = new BambuWS.WSDBSoapClient();
            var persona = WS.LogIn(correoLogin, passwordLogin);
            if (WS.LogIn(correoLogin, passwordLogin)!= null)
            {
                Session["login"] = "YES";
                Session.Timeout = 10;
                Session["UserType"] = persona.EsAdmin;
                Session["PersonaID"] = persona.PersonaID.ToString();
                Session["Cedula"] = persona.PersonaID.ToString();
                if (Session["UserType"].Equals(true))
                {
                    return RedirectToAction("ListAppointments", "Citas");
                }
                else if (Session["UserType"].Equals(false))
                {
                    return RedirectToAction("ListAppointmentsCitas", "Citas");
      
                }
                else
                {
                    Session["login"] = "NO";
                    Session.Timeout = 1;
                    ViewData["Message"] = "Credenciales Incorrectas";
                    return View("~/Views/Citas/SignIn_Up.cshtml");
                }
                
            }
            else
            {
                ViewData["Message"] = "Credenciales Incorrectas";
                return View("~/Views/Citas/SignIn_Up.cshtml");
            }
        }
    }
}