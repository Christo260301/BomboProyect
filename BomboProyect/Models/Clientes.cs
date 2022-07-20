using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class Clientes
    {
        public int ClienteId { get; set; }
        public String Nombre { get; set; }
        public String ApePat { get; set; }
        public String ApeMat { get; set; }
        public bool Status { get; set; }

       

        //Relacion con la tabla usuario
        public Usuarios Usuario { get; set; }

        //Relacion con la tabla persona
        public virtual Personas Persona { get; set; }
    }
}