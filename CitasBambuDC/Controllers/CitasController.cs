using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        /// Pagina de inicio de sesion
        /// </summary>
        /// <returns></returns>
        public ActionResult ListAppointments()
        {
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

        public ActionResult ListAppointmentsCitas()
        {
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
        /// Método para ver la Vista
        /// </summary>
        /// <returns></returns>
        public ActionResult Appointment()
        {
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
        /// Pagina de creacion de citas para los administradores
        /// </summary>
        /// <returns></returns>
        public ActionResult CrearCita()
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

        /// <summary>
        /// Pagina para reservar una cita
        /// </summary>
        /// <returns></returns>
        public ActionResult ReservarCita()
        {
            return View();
        }
       
    }
}
