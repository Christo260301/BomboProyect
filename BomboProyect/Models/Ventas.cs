using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class Ventas
    {
        public int VentaId{ get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String FechaVenta { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String HoraVenta { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public bool Status { get; set; }    

        [Required]
        public Usuarios Usuarios { get; set;}

    }
}