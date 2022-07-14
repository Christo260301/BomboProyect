using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class DetVenta
    {
        private double PrecioVenta { get; set; }
        private int Cantidad { get; set; }
        private String Unidad { get; set; }
        private Ventas Ventas { get; set; }
        private Productos Productos { get; set; }
    }
}