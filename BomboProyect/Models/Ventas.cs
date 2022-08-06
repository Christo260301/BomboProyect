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

        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public DateTime FechaVenta { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public DateTime HoraVenta { get; set; }

        public int Status { get; set; }    

        [Required]
        public Usuarios Usuarios { get; set;}

        public List<DetVenta> DetVenta { get; set; }

    }
}