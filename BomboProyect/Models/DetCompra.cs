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
        public Compras Compra { get; set; }
        public Insumos Insumos { get; set; }

        //Relacion con la tabla compra e insumos
        public int IdCompra { get; set; }
        public int IdInsumo { get; set; }
    }
}