using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class Usuario
    {
        private int IdUsuario { get; set; }
        private String Correo { get; set; }
        private String Contrasennia { get; set; }
        private bool Status { get; set; }
        private Roles Roles { get; set; }
    }
}