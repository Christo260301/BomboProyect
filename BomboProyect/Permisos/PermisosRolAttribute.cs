using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BomboProyect.Models;

namespace BomboProyect.Permisos
{
    public class PermisosRolAttribute : ActionFilterAttribute
    {
        private String idrol;

        public PermisosRolAttribute(String _idrol)
        {
            idrol = _idrol;
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (HttpContext.Current.Session["Usuario"] != null)
            {
                Usuarios usuarios = HttpContext.Current.Session["Usuario"] as Usuarios;

                int rol_id = 0;

                switch (this.idrol)
                {

                    case "Administrador":
                        rol_id = 1;
                        break;
                    case "Empleado":
                        rol_id = 2;
                        break;
                    case "Cliente":
                        rol_id = 3;
                        break;
                }

                if(usuarios.Rol.RolId != rol_id)
                {
                    filterContext.Result = new RedirectResult("~/Home/SinPermisos");
                }
            }
            base.OnActionExecuted(filterContext);   
        }
    }
}