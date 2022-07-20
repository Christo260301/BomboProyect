using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class Empleados
    {
        public int EmpleadoId { get; set; }
        public String Nombre { get; set; }
        public String ApePat { get; set; }
        public bool Status { get; set;}
        public Personas Persona { get; set; } //REACION 1 - 1
        public Usuarios Usuario { get; set; }
       
    }
}