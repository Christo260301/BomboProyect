using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class DetCompra
    {
        private double PrecioCompra { get; set; }
        private int Cantidad { get; set; }
        private String Unidad { get; set; }
        private Compra Compra { get; set; }
        private Insumos Insumos { get; set; }


    }
}