using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
       
        
        //Relacion con la tabla usuario
        [Required]
        public Usuarios Usuario { get; set; }

        public virtual Personas Persona { get; set; } //REACION 1 - 1

    }
}