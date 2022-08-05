using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using BomboProyect.Models;
using BomboProyect.Permisos;
using Roles = BomboProyect.Models.Roles;

namespace IDGS902_EXAM_BD.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            return View();
        }
         
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult SinPermisos()
        {
            ViewBag.Message = "Usted no cuenta con permisos para visualizar esta pagina";

            return View();
        }

        //Roles rol = new Roles();

        [PermisosRol(_idrol: "Cliente")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            Session["Usuario"] = null;

            return RedirectToAction("Index", "Acceso");
        }
    }
}