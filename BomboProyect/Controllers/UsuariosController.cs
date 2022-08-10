using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BomboProyect.Models;

namespace BomboProyect.Controllers
{
    public class UsuariosController : Controller
    {
        private BomboDBContext db = new BomboDBContext();

        // GET: Usuarios
        public ActionResult Index()
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            return View(db.Usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            ViewBag.Roless = new SelectList(db.RolesUsers, "RolId", "NombreRol");
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Usuarios usuarios, string Roless)
        {
            var correoU = db.Usuarios.Where(m => m.Correo == usuarios.Correo).ToList();
            if (correoU != null)
            {
                ViewBag.error = "Ese correo ya existe";
                ViewBag.Roless = new SelectList(db.RolesUsers, "RolId", "NombreRol");
                return View();
            }
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            try
            {


                using (BomboDBContext db = new BomboDBContext())
                {
                    var roles = new Roles();
                    roles.RolId = Convert.ToInt32(Roless);
                    db.RolesUsers.Attach(roles);
                    usuarios.Rol = roles;
                    db.Usuarios.Add(usuarios);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Acceso");
                }
            }
            catch (Exception)
            {
                return View();
            }

        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsuarioId,Nombre,ApePat,ApeMat,Calle,NumExt,NumInt,Colonia,CP,Ciudad,Municipio,Estado,Telefono,Correo,Contrasennia,Status")] Usuarios usuarios)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            if (ModelState.IsValid)
            {
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarios);
            db.SaveChanges();
            return RedirectToAction("Index");
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
