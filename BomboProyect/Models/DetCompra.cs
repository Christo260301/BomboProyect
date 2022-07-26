using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BomboProyect.Models
{
    public class DetCompra
    {
        public int DetCompraId { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public double PrecioCompra { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public DateTime FechaCaduca { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String Unidad { get; set; }

        [Required]
        public Compras Compra { get; set; }

        [Required]
        public Insumos Insumos { get; set; }
    }
}