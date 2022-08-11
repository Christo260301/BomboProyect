﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BomboProyect.Models
{
    public class Productos
    {
        [Key]
        public int ProductoId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String Nombre { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String Descripcion { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public String Foto { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        [Range(0, Int32.MaxValue, ErrorMessage = "El valor minino es 0")]
        public int Existencias { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public bool Status { get; set; }

        public List<DetVenta> DetVenta { get; set; }

        //public List<DetProducto> DetProducto { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "El campo '{0}' es obligatorio")]
        public HttpPostedFileBase Fotografia { get; set; }

        public void EliminarFoto(string ruta)
        {
            File.Delete(ruta);
        }
    }
}