using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class Ventas
    {
        public int VentaId{ get; set; }
        public String Fechaventa { get; set; }
        public String HoraVenta { get; set; }
        public bool Status { get; set; }
        public Clientes Cliente { get; set; }

        //Relacion con detalle venta
        public List<DetVenta> DetVenta { get; set; }
        

        //Relacion con usuario
        public Usuarios Usuarios { get; set;}

    }
}