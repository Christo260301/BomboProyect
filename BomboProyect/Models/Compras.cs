using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BomboProyect.Models
{
    public class Compras
    {
        public int ComprasId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String FechaVenta { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String HoraVenta { get; set; }
        public bool Status { get; set; }

        [Required]
        public Usuarios Usuario { get; set; }

        [Required]
        public Proveedor Proveedor { get; set; }

        public List<DetCompra> DetCompra { get; set; }


      
    }
}