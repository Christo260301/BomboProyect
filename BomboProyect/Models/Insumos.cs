using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class Insumos
    {
        public int InsumoId { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public double Precio { get; set; }
        public bool Status { get; set;}
        public String Cantidad { get; set; }
        public String Unidad { get; set; }

        //Relacion con la tabla DetCompra
        public List<DetCompra> DetCompra { get; set;}
        //Relacion con la tabla Recetas
        public List<Recetas> Recetas { get; set; }
    }
}