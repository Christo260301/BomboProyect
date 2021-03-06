using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.IO;

namespace BomboProyect.Models
{
    public class Usuarios
    {
        public int UsuarioId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String Nombre { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String ApePat { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String ApeMat { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String Calle { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String NumExt { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String NumInt { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String Colonia { get; set; }

        [StringLength(5)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String CP { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String Ciudad { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String Municipio { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String Estado { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String Telefono { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String Correo { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String Contrasennia { get; set; }
        public bool Status { get; set; }

        //Relacion con la tabla rol
        public Roles Rol { get; set; }

    }
}