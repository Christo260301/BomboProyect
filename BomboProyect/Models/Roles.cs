using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace BomboProyect.Models
{
    public class Roles
    {
        public int RolId { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String NombreRol { get; set; }
    }

    public enum Rol
    {
        Empleado = 1,
        Cliente = 2,
        Administrador = 3
    }
}