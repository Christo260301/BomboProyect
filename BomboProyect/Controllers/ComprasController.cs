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

            if (compras != null)
            {
                List<DetCompra> detCom = db.DetCompra.Where(m => m.Compra.ComprasId == id).ToList();
                ViewBag.listC = detCom;
                return View(compras);
            }
            else
            {
                return HttpNotFound();
            }
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
        public ActionResult Create([Bind(Include = "ComprasId,FechaCompra,HoraCompra,Status")] Compras compras, 
                                    string usuarioId, string Prov, List<Insumos> insumos)
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
                int contador = 0;
                
                foreach (var item in insumos)
                {
                    if (Convert.ToInt32(item.Existencias) > 0)
                    {
                        DateTime hoy = DateTime.Now;
                        contador = contador + 1;

                        var detCompra = new DetCompra();
                        var insumo = new Insumos();

                        insumo.InsumoId = Convert.ToInt32(item.InsumoId);
                        
                        db.Insumos.Attach(insumo);
                        
                        detCompra.Costo = Convert.ToInt32(item.Existencias) * item.Precio;
                        detCompra.Cantidad = Convert.ToInt32(item.Existencias);
                        detCompra.FechaCaduca = hoy.AddMonths(1);
                        detCompra.Unidad = item.Descripcion;
                        detCompra.Compra = compras;
                        detCompra.Insumos = insumo;

                        db.DetCompra.Add(detCompra);

                        using (BomboDBContext du = new BomboDBContext())
                        {
                            Insumos insu = du.Insumos.Find(item.InsumoId);
                            double existencia = Convert.ToInt32(insu.Existencias) + Convert.ToInt32(item.Existencias);
                            insu.Existencias = existencia;
                            insu.ContenidoTot = insu.ContenidoTot + (Convert.ToInt32(item.Existencias) * insu.CantidadNeta);
                            du.Insumos.Attach(insu);
                            du.Entry(insu).Property(x => x.Existencias).IsModified = true;
                            du.Entry(insu).Property(x => x.ContenidoTot).IsModified = true;
                            du.SaveChanges();

                        }
                    }
                }

                if (contador == 0)
                {
                    return RedirectToAction("Index");
                }

                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                ViewBag.usuario = db.Usuarios.Where(u => u.Rol.RolId == 1).ToList();
                ViewBag.Prov = new SelectList(db.Proveedor, "ProveedorId", "RazonSocial");

                return View(compras);
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
