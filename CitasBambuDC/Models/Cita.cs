using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasBambuDC.Models
{
    public class Cita
    {
        public int CitasID { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<int> ClienteAsignado { get; set; }
        public string Descripcion { get; set; }
        public string NombrePaciente { get; set; }

    }
}