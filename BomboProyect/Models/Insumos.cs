using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BomboProyect.Models
{
    public class Insumos
    {
        [Key]
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
        [Range(0.0, 9999.0, ErrorMessage = "El campo debe ser mayor a 0")]
        public double ContenidoTot { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public double Existencias { get; set; }

        public bool Status { get; set; }

        public List<DetCompra> DetCompra { get; set;}

        //public List<DetProducto> DetProductos { get; set; }

        [NotMapped]
        [Range(0.0, Double.MaxValue, ErrorMessage = "El campo debe ser mayor a 0")]
        public double CantProduc { get; set; }
    }
}