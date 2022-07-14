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

        //DBSETS

        //MODEL BUILDER
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}