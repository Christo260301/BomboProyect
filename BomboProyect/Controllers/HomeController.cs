using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using BomboProyect.Models;
using BomboProyect.Permisos;
    
namespace IDGS902_EXAM_BD.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private BomboDBContext db = new BomboDBContext();

        public ActionResult Index()
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            ViewBag.productos = db.Productos.Where(p => p.Status == true).ToList();
            return View(db.Productos.Where(p => p.Status == true).ToList());
        }
         
        public ActionResult About()
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult SinPermisos()
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            ViewBag.Message = "Usted no cuenta con permisos para visualizar esta pagina";

            return View();
        }

        [PermisosRol(_idrol: "Administrador")]
        public ActionResult Contact()
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            ViewBag.Message = "Escribe tus datos y cuéntanos tu inquietud.";

            return View();
        }

        public ActionResult CerrarSesion()
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            FormsAuthentication.SignOut();
            Session["Usuario"] = null;

            return RedirectToAction("Index", "Acceso");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}