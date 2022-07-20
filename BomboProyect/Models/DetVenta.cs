using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class DetVenta
    {
        public int DetVentaId { get; set; }
        public double PrecioVenta { get; set; }
        public int Cantidad { get; set; }
        public String Unidad { get; set; }
        public Ventas Ventas { get; set; }
        public Productos Productos { get; set; }


        //Relacion con Producto y Venta
        public int IdVenta { get; set; }
        public Ventas Venta { get; set; }
        public int IdProducto { get; set; }
        public  Productos Producto { get; set; }
    }
}