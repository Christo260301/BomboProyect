using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class Productos
    {
        public int ProductoId { get; set; }
        public String Nombre { get; set; }
        public String Descripcipn { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public String Unidad { get; set; }
        public bool Status { get; set; }

        //Relacion con DetVenta
        public List<DetVenta> DetVenta { get; set; }

        //Relacion con la tabla Recetas
        public List<Recetas> Recetas { get; set; }
    }
}