using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class Compra
    {
        private int IdCompra { get; set; }
        private String Proveedor { get; set; }
        private String Fechaventa { get; set; }
        private String HoraVenta { get; set; }
        private bool Status { get; set; }
        private Usuario Usuario { get; set; }
    }
}