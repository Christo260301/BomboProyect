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
    public class DetProductoesController : Controller
    {
        private BomboDBContext db = new BomboDBContext();

        // GET: DetProductoes
        public ActionResult Index()
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            return View(db.DetProductos.ToList());
        }

        // GET: DetProductoes/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetProducto detProducto = db.DetProductos.Find(id);
            if (detProducto == null)
            {
                return HttpNotFound();
            }
            return View(detProducto);
        }

        // GET: DetProductoes/Create
        public ActionResult Create()
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            return View();
        }

        // POST: DetProductoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DetProductoId,Cantidad,Unidad")] DetProducto detProducto)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            if (ModelState.IsValid)
            {
                db.DetProductos.Add(detProducto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(detProducto);
        }

        // GET: DetProductoes/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetProducto detProducto = db.DetProductos.Find(id);
            if (detProducto == null)
            {
                return HttpNotFound();
            }
            return View(detProducto);
        }

        // POST: DetProductoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DetProductoId,Cantidad,Unidad")] DetProducto detProducto)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            if (ModelState.IsValid)
            {
                db.Entry(detProducto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(detProducto);
        }

        // GET: DetProductoes/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetProducto detProducto = db.DetProductos.Find(id);
            if (detProducto == null)
            {
                return HttpNotFound();
            }
            return View(detProducto);
        }

        // POST: DetProductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            DetProducto detProducto = db.DetProductos.Find(id);
            db.DetProductos.Remove(detProducto);
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
