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
using System.Web.Script.Serialization;
using BomboProyect.Logica;
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

            List<DetProducto> detPr = db.DetProductos.Where(m => m.Productos.ProductoId == id).Include(nameof(DetProducto.Insumo)).ToList();
            ViewBag.listP = detPr;
            return View(productos);
        }
        public ActionResult _ListaInsumoProducto()
        {
            //List<DetProducto> detPr = db.DetProductos.Where(m => m.Productos.ProductoId == id).Include(nameof(DetProducto.Insumo)).ToList();
            return View(new List<Insumos>());
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
        public ActionResult Create([Bind(Include = "ProductoId,Nombre,Descripcion,Precio,Foto,Fotografia,Status")] Productos productos, List<Insumos> insumos)
        {

            var validImageTypes = new string[]
            {
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };


            ViewBag.insumos = db.Insumos.ToList();
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;

            
            if (insumos != null)
            {
                if (ModelState.IsValid)
                {
                    //Validacion de fotografia
                    if (validImageTypes.Any(productos.Fotografia.ContentType.Contains))
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

                        // SET STATUS TRUE | Existencias 0
                        productos.Status = true;
                        productos.Existencias = 0;

                        db.Productos.Add(productos);
                        int contador = 0;

                        foreach (var item in insumos)
                        {
                            if (Convert.ToDouble(item.CantProduc) > -1)
                            {
                                contador++;
                                var detProducto = new DetProducto();
                                var insumo = new Insumos();
                                //insumo.InsumoId = Convert.ToInt32(item.InsumoId);
                                insumo = db.Insumos.Find(item.InsumoId);
                                // db.Insumos.Attach(insumo);

                                detProducto.Insumo = insumo;
                                detProducto.Cantidad = Convert.ToDouble(item.CantProduc);
                                detProducto.Unidad = item.Unidad;
                                detProducto.Productos = productos;

                                db.DetProductos.Add(detProducto);

                            }
                        }

                        if (contador == 0)
                        {
                            return RedirectToAction("Index");
                        }

                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Fotografia", "El formato de imagen debe ser jpeg o png");

                        return View(productos);
                    }
                }
                else
                {
                    if (insumos.Count <= 0)
                    {
                        ViewBag.validInsumoList = true;
                    }
                }
            } else
            {
                ViewBag.validInsumoList = true;
            }

            return View(productos);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;
            ViewBag.insumos = db.Insumos.ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            ViewBag.detPro = db.DetProductos.Where(m => m.Productos.ProductoId == id).Include(nameof(DetProducto.Insumo)).ToList();
            //Productos productos = db.Productos.Where(m => m.ProductoId == id).Include(nameof(Productos.DetProducto)).Include(nameof(DetProducto.Insumo)).First();
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
        public ActionResult Edit([Bind(Include = "ProductoId,Nombre,Descripcion,Precio,Existencias,Foto,Fotografia,Status")] Productos productos, string rutaFotoAnterior, string InsumosRemovidos, List<DetProducto> insumos)
        {
            ViewBag.insumos = db.Insumos.ToList();
            ViewBag.detPro = db.DetProductos.Where(m => m.Productos.ProductoId == productos.ProductoId).Include(nameof(DetProducto.Insumo)).ToList();

            // REMOVEDOR DE CAMPOS NO OBLIGATORIOS
            if (insumos != null)
            {
                for (int i = 0; i < insumos.Count; i++)
                {
                    ModelState.Remove($"[{i}].Unidad");
                    ModelState.Remove($"[{i}].Productos");

                }
            }


            ModelState.Remove("Fotografia");
            if (ModelState.IsValid)
            {

                //MODIFICACION DE FOTOGRAFIA
                if (productos.Fotografia != null)
                {
                    string NombreArchivo = Path.GetFileNameWithoutExtension(productos.Fotografia.FileName);
                    string ExtencionArchivo = Path.GetExtension(productos.Fotografia.FileName);
                    NombreArchivo = DateTime.Now.ToString("dd_MM_yyyy") + "-" + NombreArchivo.Trim() + "-" + productos.Nombre + "-" + ExtencionArchivo;
                    string ruta = "~/ProductosImages/" + NombreArchivo;

                    if (!rutaFotoAnterior.Equals(ruta))
                    {
                        productos.Foto = ruta;
                        NombreArchivo = Path.Combine(Server.MapPath("~/ProductosImages/"), NombreArchivo);
                        productos.Fotografia.SaveAs(NombreArchivo);
                        try
                        {
                            productos.EliminarFoto(Path.Combine(Server.MapPath(rutaFotoAnterior)));
                        }
                        catch (IOException d)
                        {
                            Console.WriteLine(d.Message);
                        }

                    }
                }
                else
                {
                    // ESTO ES NECESARIO PARA CONSERVAR LA MISMA FOTO
                    string nombre = rutaFotoAnterior.Split('/')[2];
                    productos.Foto = rutaFotoAnterior;
                    byte[] bytes = System.IO.File.ReadAllBytes(Server.MapPath(productos.Foto));
                    var contentTypeFile = "image/jpeg";
                    var filename = nombre;
                    productos.Fotografia = (HttpPostedFileBase)new MemoryPostedFile(new MemoryStream(bytes), contentTypeFile, filename);
                }


                db.Entry(productos).State = EntityState.Modified;

                // ############ MODIFICACION DE DETALLE DE PRODUCTOS
                // Insumos removidos del producto
                if (InsumosRemovidos != null)
                {
                    string idInsumosRemoved = InsumosRemovidos.Substring(1, InsumosRemovidos.Length - 2);
                    string[] lstIdInsumosRemoved = idInsumosRemoved.Split(',');

                    foreach (var id in lstIdInsumosRemoved)
                    {
                        int idInsumos = Convert.ToInt32(id);

                        List<DetProducto> detProducto = db.DetProductos.Where(
                            m => m.Productos.ProductoId == productos.ProductoId && m.Insumo.InsumoId == idInsumos).ToList();

                        if (detProducto.Count > 0)
                        {
                            db.DetProductos.Remove(detProducto[0]);
                        }
                    }
                }

                // ############ MODIFICACION Y REGISTRO DE NUEVOS DETPRODUCTOS
                int contador = 0;

                foreach (var item in insumos)
                {
                    if (Convert.ToDouble(item.Insumo.CantProduc) > -1)
                    {
                        int idInsu = Convert.ToInt32(item.Insumo.InsumoId);

                        List<DetProducto> detPro = db.DetProductos.Where(
                            m => m.Productos.ProductoId == productos.ProductoId && m.Insumo.InsumoId == idInsu).ToList();

                        if (detPro.Count <= 0)
                        {
                            contador++;
                            var detProducto = new DetProducto();
                            var insumo = new Insumos();
                            insumo = db.Insumos.Find(item.Insumo.InsumoId);

                            detProducto.Insumo = insumo;
                            detProducto.Cantidad = Convert.ToDouble(item.Insumo.CantProduc);
                            detProducto.Unidad = item.Insumo.Unidad;
                            detProducto.Productos = productos;

                            db.DetProductos.Add(detProducto);
                        }
                        else
                        {

                            detPro[0].Cantidad = item.Insumo.CantProduc;
                            db.Entry(detPro[0]).State = EntityState.Modified;
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

        // GET: Productos/GenerarExistencias/5
        public ActionResult GenerarExistencias(int? id)
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

            List<DetProducto> detPr = db.DetProductos.Where(m => m.Productos.ProductoId == id).Include(nameof(DetProducto.Insumo)).ToList();
            ViewBag.listP = detPr;
            return View(productos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerarExistencias([Bind(Include = "ProductoId,Existencias")] Productos productos)
        {
            
            if (ModelState.IsValid)
            {
                int id = productos.ProductoId;
                List<String> lstMensajes = new List<String>();

                Productos prod = db.Productos.Find(id);

                List<DetProducto> detProductos = db.DetProductos.Where(m => m.Productos.ProductoId == id).ToList();

                if (detProductos.Count < 0)
                {
                    int existToGenerate = productos.Existencias;

                    List<DetProducto> insumoNecesario = new List<DetProducto>();

                    int contParaGenerar = 0;
                    int contNoGenerados = 0;

                    foreach (DetProducto dtp in detProductos)
                    {
                        dtp.Cantidad = dtp.Cantidad * existToGenerate;
                        if (dtp.Cantidad > dtp.Insumo.ContenidoTot)
                        {
                            contParaGenerar++;
                        } else
                        {
                            contNoGenerados++;
                            insumoNecesario.Add(dtp)
                        }
                    }

                    if (contParaGenerar < existToGenerate)
                    {
                        List<Dictionary<String, String>> data = new List<Dictionary<string, string>>();
                        foreach(DetProducto d in insumoNecesario)
                        {
                            Dictionary<string, string> dic = new Dictionary<string, string>();
                            dic.Add("nombreInsumo", d.Insumo.Nombre);
                            dic.Add("insumoFaltante", Convert.ToString((d.Cantidad - d.Insumo.ContenidoTot)));
                            data.Add(dic);
                        }
                        ViewBag.validateInsumoText = data;
                    } else
                    {
                        productos.Existencias = existToGenerate;
                        db.Entry(productos).State = EntityState.Modified;
                    }
                }
            }

            //if (ModelState.IsValid)
            //{
            //    int id = productos.ProductoId;
            //    Productos prod = db.Productos.Find(productos.ProductoId);
            //}

            //ViewBag.ssUsuario = HttpContext.Session["Usuario"] as Usuarios;

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Productos productos = db.Productos.Find(id);
            //if (productos == null)
            //{
            //    return HttpNotFound();
            //}

            List<DetProducto> detPr = db.DetProductos.Where(m => m.Productos.ProductoId == id).Include(nameof(DetProducto.Insumo)).ToList();
            ViewBag.listP = detPr;
            return View(productos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        protected bool validateCantProduc(List<Insumos> insumos)
        {
            foreach (var i in insumos)
            {
                if (i.CantProduc <= 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
