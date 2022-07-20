using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class DetCompra
    {
        public int DetCompraId { get; set; }
        public double PrecioCompra { get; set; }
        public int Cantidad { get; set; }
        public String Unidad { get; set; }

        //Relacion con las tablas compra e insumos
        public Compras Compra { get; set; }
        public Insumos Insumos { get; set; }
    }
}