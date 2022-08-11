using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using BomboProyect.Models;
using BomboProyect.Permisos;

namespace BomboProyect.Controllers
{
    public class ComprasController : Controller
    {
        private BomboDBContext db = new BomboDBContext();

        // GET: Compras
        public ActionResult Index()
        {

            if (Session["Usuario"] != null)
            {
                Usuarios user = Session["Usuario"] as Usuarios;
                if (user.Rol.RolId == 1)
                {
                    ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
                    return View(db.Compras.ToList());

                }
                else
                {
                    return RedirectToAction("SinPermisos", "Home");

                }
            }
            else
            {
                return RedirectToAction("SinPermisos", "Home");

            }
        }

        // GET: Compras/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;

            if (Session["Usuario"] != null)
            {
                Usuarios user = Session["Usuario"] as Usuarios;
                if (user.Rol.RolId == 1)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    var compras = db.Compras.Include(m => m.Usuario).Include(m => m.Proveedor).Where(m => m.ComprasId == id).ToList();
                    Compras compra = compras[0];

                    if (compra != null)
                    {
                        List<DetCompra> detCom = db.DetCompra.Include(m => m.Insumos).Where(m => m.Compra.ComprasId == id).ToList();
                        ViewBag.listC = detCom;
                        return View(compra);
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                else
                {
                    return RedirectToAction("SinPermisos", "Home");
                }
            }
            else
            {
                return RedirectToAction("SinPermisos", "Home");
            }
                    
        }

        public ActionResult _ListInsumos()
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            return View(db.Insumos.ToList());
        }


        // GET: Compras/Create
        public ActionResult Create()
        {
            if (Session["Usuario"] != null)
            {
                Usuarios user = Session["Usuario"] as Usuarios;
                if (user.Rol.RolId == 1)
                {
                    ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
                    ViewBag.Usuario = user.Nombre + " " + user.ApePat;
                    ViewBag.Id = user.UsuarioId;
                    ViewBag.Prov = new SelectList(db.Proveedor, "ProveedorId", "RazonSocial");
                    return View();
                }
                else
                {
                    return RedirectToAction("SinPermisos", "Home");

                }
            }
            else
            {
                return RedirectToAction("SinPermisos", "Home");
            }
        }

        // POST: Compras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComprasId,FechaCompra,HoraCompra,Status")] Compras compras,
                                    string usuarioId, string Prov, List<Insumos> insumos)
        {
            var sess = HttpContext.Session["Usuario"] as Usuarios;
            ViewBag.ssUsuario = sess;
            ViewBag.Usuario = sess.Nombre + " " + sess.ApePat;
            ViewBag.Id = sess.UsuarioId;
            ViewBag.Prov = new SelectList(db.Proveedor, "ProveedorId", "RazonSocial");

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

                if (insumos != null)
                {
                    db.Compras.Add(compras);
                    int contador = 0;
                    using (BomboDBContext du = new BomboDBContext())
                    {
                        foreach (var item in insumos)
                        {
                            if (Convert.ToInt32(item.CantProduc) > 0)
                            {
                                contador = contador + 1;

                                var detCompra = new DetCompra();
                                //var insumo = new Insumos();
                                var insumo = item;

                                //insumo.InsumoId = Convert.ToInt32(item.InsumoId);

                                db.Insumos.Attach(insumo);

                                detCompra.Costo = Convert.ToInt32(item.CantProduc) * item.Precio;
                                detCompra.Cantidad = Convert.ToInt32(item.CantProduc);
                                detCompra.FechaCaduca = item.FechaCad;
                                detCompra.Unidad = item.Unidad;
                                detCompra.Compra = compras;
                                detCompra.Insumos = insumo;

                                db.DetCompra.Add(detCompra);

                                Insumos insu = du.Insumos.Find(item.InsumoId);
                                double existencia = Convert.ToInt32(insu.Existencias) + Convert.ToInt32(item.CantProduc);
                                insu.Existencias = existencia;
                                insu.ContenidoTot = insu.ContenidoTot + (Convert.ToInt32(item.CantProduc) * insu.CantidadNeta);
                                du.Insumos.Attach(insu);
                                du.Entry(insu).Property(x => x.Existencias).IsModified = true;
                                du.Entry(insu).Property(x => x.ContenidoTot).IsModified = true;

                            }
                        }
                        if (contador == 0)
                        {
                            ViewBag.notif = "No se puede registrar una compra sin insumos";
                            return View(compras);
                        }

                        db.SaveChanges();
                        du.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ViewBag.notif = "No se puede registrar una compra sin insumos";
                    return View(compras);

                }

            }
            catch (Exception ex)
            {
                ViewBag.Prov = new SelectList(db.Proveedor, "ProveedorId", "RazonSocial");
                ViewBag.notif = "Error";
                return View(compras);
            }
        }

        public ActionResult Cancelar(int? id)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var compra = db.Compras.Include(m => m.Usuario).Include(m => m.Proveedor).Where(m => m.ComprasId == id).ToList();
            Compras compras = compra[0];

            if (compras != null)
            {
                List<DetCompra> detCom = db.DetCompra.Include(m => m.Insumos).Where(m => m.Compra.ComprasId == id).ToList();
                ViewBag.listC = detCom;
                return View(compras);
            }
            else
            {
                return HttpNotFound();
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancelar(int id, bool Status, List<Insumos> insumos)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            try
            {
                Compras compra = db.Compras.Find(id);
                compra.Status = Status;
                db.Compras.Attach(compra);
                using (BomboDBContext du = new BomboDBContext())
                {
                    foreach (var item in insumos) {
                        Insumos insu = du.Insumos.Find(item.InsumoId);
                        if (insu.Existencias >= item.Existencias && insu.ContenidoTot >= item.ContenidoTot)
                        {
                            insu.Existencias = Convert.ToInt32(insu.Existencias) - Convert.ToInt32(item.Existencias);
                            insu.ContenidoTot = insu.ContenidoTot - (Convert.ToInt32(item.Existencias) * insu.CantidadNeta);
                        }
                        du.Insumos.Attach(insu);
                        du.Entry(insu).Property(x => x.Existencias).IsModified = true;
                        du.Entry(insu).Property(x => x.ContenidoTot).IsModified = true;

                    }
                    du.SaveChanges();
                }

                db.Entry(compra).Property(x => x.Status).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            } 
            catch(Exception)
            {
                return RedirectToAction("Index");
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
