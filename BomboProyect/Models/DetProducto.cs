using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BomboProyect.Models
{
    public class DetProducto
    {
        public int DetProductoId { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public int Cantidad { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String Unidad { get; set; }

        [Required]
        public Insumos Insumo { get; set; }

        [Required]
        public Productos Productos { get; set; }
    }
}