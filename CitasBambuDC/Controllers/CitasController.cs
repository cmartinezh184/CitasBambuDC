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
                cita.NombrePaciente = datos.NombrePaciente;
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
            return View("~/Views/Citas/ListAppointmentsCitas.cshtml", listaCitas);
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
        /// Vista para que el Admin Agrege nuevos espacios de Citas
        /// </summary>
        /// <returns></returns>
        public ActionResult NombredelaNuevaVista()// en el espacio NombredelaNuevaVista se coloca el nombre que le puso kevin a la Nueva Vista
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
            return View();
        }

        [HttpPost]
        public ActionResult CrearNuevaCita(DateTime date)// estas variables vienen de la Vista
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
            WS.CrearCita(date);
            return RedirectToAction("ListAppointments", "Citas");
        }

        public ActionResult AddAppointmentAdmin()
        {
            return View();
        }

        /// <summary>
        /// Pagina de borrado de citas
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BorrarCita(string idCita)//el id extraido de la vista para borrar la Cita
        {
            BambuWS.WSDBSoapClient WS = new BambuWS.WSDBSoapClient();
            int idInt = Convert.ToInt32(idCita);
            WS.BorrarCita(idInt);
            return RedirectToAction("ListAppointments", "Citas");
        }

        /// <summary>
        /// Pagina de borrado de citas
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LiberarCita(string idCita)//el id extraido de la vista para borrar la Cita
        {
            BambuWS.WSDBSoapClient WS = new BambuWS.WSDBSoapClient();
            int idInt = Convert.ToInt32(idCita);
            WS.LiberarCita(idInt);
            return RedirectToAction("ListAppointmentsCitas", "Citas");
        }
    }
}
