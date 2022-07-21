﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BomboProyect.Models
{
    public class Recetas
    {
        public int RecetaId { get; set; }
        public int Cantidad { get; set; }
        public String Unidad { get; set; }

        [Required]
        public Insumos Insumo { get; set;}

        [Required]
        public Productos Producto { get; set; }
    }
}