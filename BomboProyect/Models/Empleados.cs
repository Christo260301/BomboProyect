using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class Empleados
    {
        private int IdEmpleado { get; set; }
        private String Nombre { get; set; }
        private String ApePat { get; set; }
        private bool Status { get; set;}
        private Personas Persona { get; set; } //REACION 1 - 1
        private Usuario Usuario { get; set; }


    }
}