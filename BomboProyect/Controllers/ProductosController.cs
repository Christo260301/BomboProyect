using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BomboProyect.Models;

namespace BomboProyect.Controllers
{
    public class ProductosController : Controller
    {
        private BomboDBContext db = new BomboDBContext();

        // GET: Productos
        public ActionResult Index()
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            return View(db.Productos.ToList());
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            ViewBag.insumos = db.Insumos.ToList();
            return View();
        }

        // POST: Productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductoId,Nombre,Descripcion,Precio,Existencias,Foto,Fotografia,Status")] Productos productos)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            if (ModelState.IsValid)
            {
                //Almacenamiento de imagenes
                string NombreArchivo = Path.GetFileNameWithoutExtension(productos.Fotografia.FileName);
                //Obtener la extencion del archivo
                string ExtencionArchivo = Path.GetExtension(productos.Fotografia.FileName);
                //Agregar la fecha actual al nombre del archivo
                NombreArchivo = DateTime.Now.ToString("dd_MM_yyyy") + "-" + NombreArchivo.Trim() + "-" + productos.Nombre + "-" + ExtencionArchivo;
                //Obtener ruta de almacenamiento de las fotografias
                //string updatePath = ConfigurationManager.AppSettings["ProductosImagePath"].ToString();
                productos.Foto = "~/ProductosImages/" + NombreArchivo;
                NombreArchivo = Path.Combine(Server.MapPath("~/ProductosImages/"), NombreArchivo);
                productos.Fotografia.SaveAs(NombreArchivo);

                db.Productos.Add(productos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productos);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductoId,Nombre,Descripcion,Precio,Foto,Existencias,Status")] Productos productos)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            if (ModelState.IsValid)
            {
                db.Entry(productos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productos);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            Productos productos = db.Productos.Find(id);
            db.Productos.Remove(productos);
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
