using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Reflection;

using EFLab.DAL.BizObjects;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

namespace EFLab.DAL
{
    public class DbEntitiesTypeB : DbContext
    {
        public DbEntitiesTypeB()
            : base("name=EFLab")
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            Database.SetInitializer<DbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<TopLevelObject> TopLevelObject { get; set; }
        public virtual DbSet<EFLab.DAL.BizObjects.TypeB.TypeBSecondLevelObject> SecondLevelObject { get; set; }
        public virtual DbSet<EFLab.DAL.BizObjects.TypeB.TypeBObject1> Object1 { get; set; }
    }
}
