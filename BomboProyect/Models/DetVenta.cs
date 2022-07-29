using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BomboProyect.Models
{
    public class DetVenta
    {
        public int DetVentaId { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public double PrecioVenta { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public int Cantidad { get; set; }

        [Required]
        public Ventas Venta { get; set; }

        [Required]
        public  Productos Producto { get; set; }
    }
}