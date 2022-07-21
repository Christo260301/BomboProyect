using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BomboProyect.Models
{
    public class BomboDBContext : DbContext
    {
        private const string ConnectionString = "DefaultConnection";

        public BomboDBContext() : base(ConnectionString)
        {

        }

        //DBsets para agregar los modelos a la bd
        public DbSet<Recetas> Recetas { get; set; }
        public DbSet<Compras> Compras { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Ventas> Ventas { get; set; }
        public DbSet<Insumos> Insumos { get; set; }
        public DbSet<DetCompra> DetCompra { get; set; }
        public DbSet<DetVenta> DetVenta { get; set; }
        public DbSet<Roles> Roles { get; set; }


        //MODEL BUILDER
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<int>().Where(p => p.Name.StartsWith("RecetaId")).Configure(p => p.IsKey());
            modelBuilder.Properties<int>().Where(p => p.Name.StartsWith("DetVentaId")).Configure(p => p.IsKey());
            modelBuilder.Properties<int>().Where(p => p.Name.StartsWith("DetCompraId")).Configure(p => p.IsKey());
            modelBuilder.Properties<int>().Where(p => p.Name.StartsWith("ComprasId")).Configure(p => p.IsKey());
            modelBuilder.Properties<int>().Where(p => p.Name.StartsWith("InsumoId")).Configure(p => p.IsKey());
            modelBuilder.Properties<int>().Where(p => p.Name.StartsWith("ProductoId")).Configure(p => p.IsKey());
            modelBuilder.Properties<int>().Where(p => p.Name.StartsWith("RolId")).Configure(p => p.IsKey());
            modelBuilder.Properties<int>().Where(p => p.Name.StartsWith("UsuarioId")).Configure(p => p.IsKey());
            modelBuilder.Properties<int>().Where(p => p.Name.StartsWith("VentaId")).Configure(p => p.IsKey());

            base.OnModelCreating(modelBuilder);
        }

       


    }
}