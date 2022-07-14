using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class Productos
    {
        private int IdProducto { get; set; }
        private String Nombre { get; set; }
        private String Descripcipn { get; set; }
        private double Precio { get; set; }
        private int Cantidad { get; set; }
        private String Unidad { get; set; }
        private bool Status { get; set; }
    }
}