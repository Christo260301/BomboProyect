using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace BomboProyect.Models
{
    public class Proveedor
    {
        public int ProveedorId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String RazonSocial { get; set; }

    }
}