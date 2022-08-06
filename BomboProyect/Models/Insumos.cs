using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BomboProyect.Models
{
    public class Insumos
    {
        public int InsumoId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String Nombre { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String Descripcion { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public double Precio { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String Unidad { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public double CantidadNeta { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public double ContenidoTot { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public double Existencias { get; set; }

        public bool Status { get; set; }

        public List<DetCompra> DetCompra { get; set;}

        public List<DetProducto> DetProductos { get; set; }
    }
}