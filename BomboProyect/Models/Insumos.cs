using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class Insumos
    {
        private int IdInsumo { get; set; }
        private String Nombre { get; set; }
        private String Descripcion { get; set; }
        private bool Status { get; set;}
        private String Cantidad { get; set; }
        private String Unidad { get; set; }
    }
}