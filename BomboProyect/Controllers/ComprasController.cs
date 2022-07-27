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
    public class ComprasController : Controller
    {
        private BomboDBContext db = new BomboDBContext();

        // GET: Compras
        public ActionResult Index()
        {
            return View(db.Compras.ToList());
        }

        // GET: Compras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            return View(compras);
        }

        public ActionResult _ListInsumos()
        {
            return View(db.Insumos.ToList());
        }


        // GET: Compras/Create
        public ActionResult Create()
        {
            ViewBag.usuario = db.Usuarios.Where(u => u.Rol.RolId == 1).ToList();
            ViewBag.Prov = new SelectList(db.Proveedor, "ProveedorId", "RazonSocial");
            return View();
        }

        // POST: Compras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComprasId,FechaVenta,HoraVenta,Status")] Compras compras, 
                                    string usuarioId, string Prov,List<Insumos> insumos)
        {
            try
            {
                var user = new Usuarios();
                var prov = new Proveedor();
                prov.ProveedorId = Convert.ToInt32(Prov);
                user.UsuarioId = Convert.ToInt32(usuarioId);
                db.Proveedor.Attach(prov);
                db.Usuarios.Attach(user);

                compras.Proveedor = prov;
                compras.Usuario = user;
                
                db.Compras.Add(compras);
                foreach (var item in insumos)
                {
                    DetCompra detCompra = new DetCompra();
                    detCompra.Compra = compras;
                    detCompra.Insumos = item;
                    db.DetCompra.Add(detCompra);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
                
            }
            catch (Exception)
            {
                ViewBag.usuario = db.Usuarios.Where(u => u.Rol.RolId == 1).ToList();
                ViewBag.Prov = new SelectList(db.Proveedor, "ProveedorId", "RazonSocial");

                return View();
            }
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
