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
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            Usuarios usr = new Usuarios();
            usr = Session["Usuario"] as Usuarios;
            if (usr.Rol.RolId==3|| usr.Rol.RolId==2)
            {
                return View(db.Ventas.ToList());
            }
            else {
                return View(db.Ventas.Where(v => v.Usuarios.UsuarioId == usr.UsuarioId));
            }
            
        }

        // GET: Ventas/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ventas ventas = db.Ventas.Find(id);

            if (ventas != null)
            {

                List<DetVenta> detVen = db.DetVenta.Where(m => m.Venta.VentaId == id).ToList();
                List<Productos> prods = db.Productos.SqlQuery("SELECT * FROM Productos INNER JOIN DetVentas ON Productos.ProductoId = DetVentas.Producto_ProductoId").ToList();
                ViewBag.listV = detVen;
                ViewBag.listP = prods;
                Usuarios usr = db.Usuarios.SqlQuery("SELECT * FROM Usuarios INNER JOIN Ventas ON Usuarios.UsuarioId = Ventas.Usuarios_UsuarioId").First();                
                ViewBag.nombreUser = usr.Nombre;
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
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            Usuarios user = new Usuarios();
            user = Session["Usuario"] as Usuarios;
            ViewBag.Usuario = user.Nombre;
            ViewBag.Id = user.UsuarioId;
            return View();
        }

        // POST: Ventas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VentaId,Fechaventa,HoraVenta,Status")] Ventas ventas,List<Productos> productos)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            try {
                if (Session["Usuario"] != null)
                {   //Ventas
                    Usuarios usuario = Session["Usuario"] as Usuarios;
                    var user = new Usuarios();
                    user.UsuarioId = usuario.UsuarioId;
                    db.Usuarios.Attach(user);
                    ventas.Usuarios = user;
                    db.Ventas.Add(ventas);

                    int contador = 0;
                    foreach (var item in productos)
                    {
                        if (Convert.ToInt32(item.Existencias) > 0)
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

                            using (BomboDBContext dp = new BomboDBContext())
                            {
                                Productos prod = dp.Productos.Find(item.ProductoId);
                                int exist = prod.Existencias - item.Existencias;
                                prod.Existencias = exist;
                                dp.Productos.Attach(prod);
                                dp.Entry(prod).Property(x => x.Existencias).IsModified = true;
                                dp.SaveChanges();
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
                
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.usuario = e.ToString();                

                return View();
            }


        }

        public ActionResult _ListProductos() 
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
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
