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
        private Roles idrol;

        public PermisosRolAttribute(Roles _idrol)
        {
            idrol.RolId = _idrol.RolId;
            //idrol.RolId = Int32.Parse(_idrol.RolId.ToString());
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (HttpContext.Current.Session["Usuario"] != null)
            {
                Usuarios usuarios = HttpContext.Current.Session["Usuario"] as Usuarios;

                if(usuarios.Rol.RolId != this.idrol.RolId)
                {
                    filterContext.Result = new RedirectResult("~/Home/SinPermiso");
                }
            }
            base.OnActionExecuted(filterContext);   
        }
    }
}