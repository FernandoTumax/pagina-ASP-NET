using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebInformacionDocenteJT.Models
{
    public class Docente
    {
        public int IdDocente { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public decimal Salario { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}