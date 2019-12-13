using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View("~/Views/Citas/ListAppointments.cshtml");
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
