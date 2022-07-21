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
        public String Fechaventa { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String HoraVenta { get; set; }
        public bool Status { get; set; }    

        //Relacion con usuario
        [Required]
        public Usuarios Usuarios { get; set;}

    }
}