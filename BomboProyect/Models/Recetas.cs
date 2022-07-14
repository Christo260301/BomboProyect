using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class Recetas
    {
        private int Cantidad { get; set; }
        private String Unidad { get; set; }
        private Productos Productos { get; set; }
        private List<Insumos> Insumos { get; set; }    
    }
}