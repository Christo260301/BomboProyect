using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BomboProyect.Logica;
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
                List<Productos> prods = db.Productos.SqlQuery("SELECT * FROM Productos INNER JOIN DetVentas ON Productos.ProductoId = DetVentas.Producto_ProductoId WHERE DetVentas.Venta_VentaId =" + id).ToList();
                ViewBag.listV = detVen;
                ViewBag.listP = prods;
                Usuarios usr = db.Usuarios.SqlQuery("SELECT * FROM Usuarios INNER JOIN Ventas ON Usuarios.UsuarioId = Ventas.Usuarios_UsuarioId WHERE Ventas.VentaId ="+id).First();                
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
        public ActionResult Create([Bind(Include = "VentaId,Fechaventa,HoraVenta")] Ventas ventas,List<Productos> productos)
        {

            ModelState.Remove("Usuarios");
            
            if (productos.Count > 0)
            {
                for(int i = 0; i < productos.Count; i++)
                {
                    ModelState.Remove($"[{i}].Nombre");
                    ModelState.Remove($"[{i}].Descripcion");
                    ModelState.Remove($"[{i}].Foto");
                    ModelState.Remove($"[{i}].Fotografia");
                }
            }

            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            try {
                if (Session["Usuario"] != null)
                {   //Ventas
                    Usuarios usuario = Session["Usuario"] as Usuarios;
                    var user = new Usuarios();
                    user.UsuarioId = usuario.UsuarioId;
                    db.Usuarios.Attach(user);
                    ventas.Usuarios = user;
                    ventas.Status = 1; // ACTIVADA
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

                                // ESTO ES NECESARIO PARA CONSERVAR LA MISMA FOTO
                                string nombre = prod.Foto.Split('/')[2];
                                byte[] bytes = System.IO.File.ReadAllBytes(Server.MapPath(prod.Foto));
                                var contentTypeFile = "image/jpeg";
                                var filename = nombre;
                                prod.Fotografia = (HttpPostedFileBase)new MemoryPostedFile(new MemoryStream(bytes), contentTypeFile, filename);

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

        public ActionResult Cancelar(int? id)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var venta = db.Ventas.Include(m => m.Usuarios).Include(m => m.DetVenta).Where(m => m.VentaId == id).ToList();
            Ventas ventas = venta[0];

            if (ventas != null)
            {
                ventas.Status = 2;
                db.Entry(ventas).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Terminar(int? id)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var venta = db.Ventas.Include(m => m.Usuarios).Include(m => m.DetVenta).Where(m => m.VentaId == id).ToList();
            Ventas ventas = venta[0];

            if (ventas != null)
            {
                ventas.Status = 3;
                db.Entry(ventas).State = EntityState.Modified;

                db.SaveChanges();

                Usuarios usr = new Usuarios();
                usr = Session["Usuario"] as Usuarios;
                if (usr.Rol.RolId == 3 || usr.Rol.RolId == 2)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
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
