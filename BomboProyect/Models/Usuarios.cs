using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class Usuarios
    {
        public int UsuarioId { get; set; }
        public String Correo { get; set; }
        public String Contrasennia { get; set; }
        public bool Status { get; set; }        

        
        //Relacion con la tabla rol
        public Roles Rol { get; set; }
       
    }
}