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

        //public ActionResult LogIn()
        //{
        //    if (Session["UserType"] != null)
        //    {
        //        return RedirectToAction("");
        //    }
        //    return View();
        //}

        //public ActionResult AdminLogIn()
        //{
        //    if (Session["UserType"] != null)
        //    {
        //        return RedirectToAction("");
        //    }
        //    return View();
        //}

        //public ActionResult LogOut()
        //{
        //    if (Session["UserType"] != null)
        //    {
        //        Session["UserType"] = null;
        //    }
        //    return RedirectToAction("");
        //}

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
            var persona = WS.LogIn(correoLogin, passwordLogin);
            if (WS.LogIn(correoLogin, passwordLogin)!= null)
            {
                Session["UserType"] = persona.EsAdmin;
                Session["PersonaID"] = persona.PersonaID.ToString();
                Session["Cedula"] = persona.PersonaID.ToString();
                if (Session["UserType"].Equals(false))
                {

                }
                else
                {

                }
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