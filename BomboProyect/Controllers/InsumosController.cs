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
    public class InsumosController : Controller
    {
        private BomboDBContext db = new BomboDBContext();

        // GET: Insumos
        public ActionResult Index()
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            ViewBag.activos = true;
            return View(db.Insumos.Where(i => i.Status == true).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string actInac)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            List<Insumos> insumos = new List<Insumos>();
            if (actInac.Equals("INACTIVOS"))
            {
                ViewBag.activos = false;
                insumos = db.Insumos.Where(i => i.Status == false).ToList();
            } else
            {
                ViewBag.activos = true;
                insumos = db.Insumos.Where(i => i.Status == true).ToList();
            }

            
            return View(insumos);
        }

        // GET: Insumos/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insumos insumos = db.Insumos.Find(id);
            if (insumos == null)
            {
                return HttpNotFound();
            }
            return View(insumos);
        }

        // GET: Insumos/Create
        public ActionResult Create()
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            return View();
        }

        // POST: Insumos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InsumoId,Nombre,Descripcion,Precio,Unidad,CantidadNeta,ContenidoTot,Existencias,Status")] Insumos insumos)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            insumos.Status = true;
            insumos.ContenidoTot = 0;
            insumos.Existencias = 0;
            if (ModelState.IsValid)
            {
                db.Insumos.Add(insumos);
                db.SaveChanges();
                return RedirectToAction("Index", "Insumos");
            }

            return View(insumos);
        }

        // GET: Insumos/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insumos insumos = db.Insumos.Find(id);
            if (insumos == null)
            {
                return HttpNotFound();
            }
            return View(insumos);
        }

        // POST: Insumos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InsumoId,Unidad,Nombre,Descripcion,Precio,CantidadNeta,ContenidoTot,Existencias,Status")] Insumos insumos)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;

            //ModelState.Remove("Unidad");

            if (ModelState.IsValid)
            {
                db.Entry(insumos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insumos);
        }

        // GET: Insumos/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insumos insumos = db.Insumos.Find(id);
            if (insumos == null)
            {
                return HttpNotFound();
            }
            return View(insumos);
        }

        // POST: Insumos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            Insumos insumos = db.Insumos.Find(id);
            insumos.Status = false;
            db.Entry(insumos).State = EntityState.Modified;
            //db.Insumos.Remove(insumos);
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
