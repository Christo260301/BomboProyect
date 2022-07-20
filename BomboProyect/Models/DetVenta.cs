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


        //Relacion con Producto y Venta        
        public Ventas Venta { get; set; }
       
        public  Productos Producto { get; set; }
    }
}