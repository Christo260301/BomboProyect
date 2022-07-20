using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class Personas
    {
        public int PersonaId { get; set; }
        public String Calle { get; set; }
        public String NumExt { get; set; }
        public String Colonia { get; set; }
        public String CP { get; set; }
        public String Ciudad { get; set; }
        public String Estado { get; set; }
        public String Telefono { get; set; }

        //Relacion con tabla cliente
        public Clientes Cliente { get; set; }

        //Relacion con la tabla empleado
        public Empleados Empleado { get; set; }


    }
}