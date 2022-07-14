using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class Clientes
    {
        private int IdCliente { get; set; }
        private String Nombre { get; set; }
        private String ApePat { get; set; }
        private bool Status { get; set; }
        private Personas Persona { get; set; }
        private Usuario Usuario { get; set; }
    }
}