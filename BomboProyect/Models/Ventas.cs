using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class Ventas
    {
        private int IdVenta { get; set; }
        private String Fechaventa { get; set; }
        private String HoraVenta { get; set; }
        private bool Status { get; set; }
        private Clientes Clientes { get; set; }

    }
}