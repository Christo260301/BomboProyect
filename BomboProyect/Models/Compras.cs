using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class Compras
    {
        public int ComprasId { get; set; }
        public String Proveedor { get; set; }
        public String Fechaventa { get; set; }
        public String HoraVenta { get; set; }
        public bool Status { get; set; }
        public Usuarios Usuario { get; set; }

        //Relacion con la tabla DetCompra
        public List<DetCompra> DetCompra { get; set; }
      
    }
}