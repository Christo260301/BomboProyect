using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BomboProyect.Models
{
    public class Usuarios
    {
        public int UsuarioId { get; set; }
        public String Correo { get; set; }
        public String Contrasennia { get; set; }
        public bool Status { get; set; }
        public Roles Roles { get; set; }

        
        //Relacion con la tabla rol
        public Roles Rol { get; set; }

        //Relacion con la tabla cliente
        public Clientes Cliente { get; set;}

        //Relacion con la tabla compra
        public Compras Compra { get; set; }
        //Relacion con la tabla empleado
        public Empleados Empleado { get; set; }

        //Relacion con la tabla venta
        public Ventas Venta { get; set; }
    }
}