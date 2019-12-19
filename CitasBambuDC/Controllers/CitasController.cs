using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CitasBambuDC.Models;

namespace CitasBambuDC.Controllers
{
    public class CitasController : Controller
    {
        /// <summary>
        /// Pagina principal del sitio de reserva de citas
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Listas para el Admin
        /// </summary>
        /// <returns></returns>
        public ActionResult ListAppointments()
        {
            bool IsPostBack = false;
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                Response.Cache.SetAllowResponseInBrowserHistory(false);
                Response.Cache.SetNoStore();
            }
            if (Convert.ToString(Session["login"]) != "YES")
            {
                Session.Abandon();
                FormsAuthentication.SignOut();
                return View("~/Views/Citas/SignIn_Up.cshtml");
            }
            Session["StartTime"] = System.DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            BambuWS.WSDBSoapClient WS = new BambuWS.WSDBSoapClient();
            var citas = WS.ListaDeCitas();
            
            List<Cita> listaCitas = new List<Cita>();

            foreach (var datos in citas)
            {
                Cita cita = new Cita();
                cita.CitasID = datos.CitasID;
                cita.ClienteAsignado = datos.ClienteAsignado;
                cita.Fecha = datos.Fecha;
                cita.Descripcion = datos.Descripcion;
                listaCitas.Add(cita);
            }
            return View("~/Views/Citas/ListAppointments.cshtml", listaCitas);
        }
        /// <summary>
        /// Listas para los Clientes
        /// </summary>
        /// <returns></returns>
        public ActionResult ListAppointmentsCitas()
        {
            bool IsPostBack = false;
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                Response.Cache.SetAllowResponseInBrowserHistory(false);
                Response.Cache.SetNoStore();
            }
            if (Convert.ToString(Session["login"]) != "YES")
            {
                Session.Abandon();
                FormsAuthentication.SignOut();
                return View("~/Views/Citas/SignIn_Up.cshtml");
            }
            Session["StartTime"] = System.DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            BambuWS.WSDBSoapClient WS = new BambuWS.WSDBSoapClient();
            var citas = WS.ListaDeCitas();

            List<Cita> listaCitas = new List<Cita>();

            foreach (var datos in citas)
            {
                Cita cita = new Cita();
                cita.CitasID = datos.CitasID;
                cita.ClienteAsignado = datos.ClienteAsignado;
                cita.Fecha = datos.Fecha;
                cita.Descripcion = datos.Descripcion;
                listaCitas.Add(cita);
            }
            return View("~/Views/Citas/ListAppointmentsClient.cshtml", listaCitas);
        }

        /// <summary>
        /// Método para ver la Vista de Reservar
        /// </summary>
        /// <returns></returns>
        public ActionResult Appointment()
        {
            bool IsPostBack = false;
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                Response.Cache.SetAllowResponseInBrowserHistory(false);
                Response.Cache.SetNoStore();
            }
            if (Convert.ToString(Session["login"]) != "YES")
            {
                Session.Abandon();
                FormsAuthentication.SignOut();
                return View("~/Views/Citas/SignIn_Up.cshtml");
            }
            Session["StartTime"] = System.DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            BambuWS.WSDBSoapClient WS = new BambuWS.WSDBSoapClient();
            var citas = WS.ListaDeCitas();

            List<Cita> listaCitas = new List<Cita>();

            foreach (var datos in citas)
            {
                Cita cita = new Cita();
                cita.CitasID = datos.CitasID;
                cita.ClienteAsignado = datos.ClienteAsignado;
                cita.Fecha = datos.Fecha;
                cita.Descripcion = datos.Descripcion;
                if (datos.ClienteAsignado == null)
                {
                    listaCitas.Add(cita);
                }
            }
            return View(listaCitas);
        }

        /// <summary>
        /// Método para que el Cliente Reserve una Cita
        /// </summary>
        /// <param name="cedula"></param>
        /// <param name="fecha"></param>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CrearAppointment(string cedula, DateTime fecha, string descripcion)
        {
            BambuWS.WSDBSoapClient WS = new BambuWS.WSDBSoapClient();
            var citas = WS.ListaDeCitas();
            int cedulaint = Convert.ToInt32(cedula);
            foreach (var datos in citas)
            {
                if (datos.Fecha == fecha)
                {
                    WS.ReservarCita(cedulaint,datos.CitasID,descripcion);
                }
            }
            return RedirectToAction("ListAppointmentsCitas", "Citas");
        }

        /// <summary>
        /// Metodo para cerrar la sesion del usuario
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            return View();
        }

        /// <summary>
        /// Pagina de borrado o cancelado de citas
        /// </summary>
        /// <returns></returns>
        public ActionResult CancelarCita()
        {
            return View();
        }       
    }
}
