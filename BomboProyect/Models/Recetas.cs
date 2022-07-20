using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class Recetas
    {
        public int RecetaId { get; set; }
        public int Cantidad { get; set; }
        public String Unidad { get; set; }      
       
        
        public Insumos Insumo { get; set;}
        public Productos Producto { get; set; }
    }
}