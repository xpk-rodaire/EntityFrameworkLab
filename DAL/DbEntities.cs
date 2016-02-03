using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Reflection;

using EFLab.DAL.BizObjects;
using EFLab.DAL.BizObjects.TypeA;
using EFLab.DAL.BizObjects.TypeB;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFLab.DAL
{
    public class DbEntities : DbContext
    {
        public DbEntities()
            : base("name=EFLab")
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            Database.SetInitializer<DbEntities>(new DbInitializer());
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<SecondLevelObjectBase>()
        //        .Property(c => c.SecondLevelObjectBaseId)
        //        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

        //    modelBuilder.Entity<TypeASecondLevelObject>().Map(m =>
        //    {
        //        m.MapInheritedProperties();
        //        m.ToTable("t_SecondLevelObjectA", "TypeA");
        //    });

        //    modelBuilder.Entity<TypeBSecondLevelObject>().Map(m =>
        //    {
        //        m.MapInheritedProperties();
        //        m.ToTable("t_SecondLevelObjectB", "TypeB");
        //    });

        //    base.OnModelCreating(modelBuilder);
        //}


        public virtual DbSet<TopLevelObject> TopLevelObject { get; set; }
        public virtual DbSet<SecondLevelObjectBase> SecondLevelObject { get; set; }
    }
}
