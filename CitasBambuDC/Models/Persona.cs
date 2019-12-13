using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasBambuDC.Models
{
    public class Persona
    {
        public int PersonaID { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public bool EsAdmin { get; set; }
    }
}