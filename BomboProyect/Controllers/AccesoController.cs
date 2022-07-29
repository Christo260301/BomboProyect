using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BomboProyect.Models;
using BomboProyect.Logica;
using System.Web.Security;

namespace BomboProyect.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string contrasennia)
        {
            Usuarios objeto = new Logica_Usuario().EncontrarUsuario(correo, contrasennia);

            if (objeto.Nombre != null)
            {

                FormsAuthentication.SetAuthCookie(objeto.Correo, false);

                Session["Usuario"] = objeto;

                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}