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
    public class VentasController : Controller
    {
        private BomboDBContext db = new BomboDBContext();

        // GET: Ventas
        public ActionResult Index()
        {
            return View(db.Ventas.ToList());
        }

        // GET: Ventas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ventas ventas = db.Ventas.Find(id);

            if (ventas != null)
            {

                List<DetVenta> detVen = db.DetVenta.Where(m => m.Venta.VentaId == id).ToList();
                ViewBag.listV = detVen;
                return View(ventas);
            }
            else
            {
                return HttpNotFound();
            }

        }

        // GET: Ventas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ventas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VentaId,Fechaventa,HoraVenta,Status")] Ventas ventas,List<Productos> productos)
        {
            try {
                if (Session["Usuario"] != null)
                {   //Ventas
                    Usuarios usuario = Session["Usuario"] as Usuarios;
                    db.Usuarios.Attach(usuario);
                    ventas.Usuarios = usuario;
                    db.Ventas.Add(ventas);

                    int contador = 0;
                    foreach (var item in productos)
                    {
                        if (item.Existencias > 0)
                        {
                            contador = contador + 1;
                            var detVenta = new DetVenta();
                            var producto = new Productos();

                            producto.ProductoId = item.ProductoId;
                            db.Productos.Attach(producto);

                            detVenta.PrecioVenta = item.Existencias * item.Precio;
                            detVenta.Cantidad = item.Existencias;
                            detVenta.Venta = ventas;
                            detVenta.Producto = producto;

                            db.DetVenta.Add(detVenta);

                            using (BomboDBContext du = new BomboDBContext())
                            {
                                Productos product = du.Productos.Find(item.ProductoId);
                                int existencia = product.Existencias - item.Existencias;
                                product.Existencias = existencia;
                                du.Entry(product).State = EntityState.Modified;
                                du.SaveChanges();

                                if (contador == 0)
                                {
                                    return RedirectToAction("Index");
                                }

                                db.SaveChanges();
                                return RedirectToAction("Index");


                            }
                        }
                    }

                }
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                ViewBag.usuario = db.Usuarios.Where(u => u.Rol.RolId == 1).ToList();
                ViewBag.Prov = new SelectList(db.Proveedor, "ProveedorId", "RazonSocial");

                return View(ventas);
            }


        }

        public ActionResult _ListProductos() {
            return View(db.Productos.ToList());
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
