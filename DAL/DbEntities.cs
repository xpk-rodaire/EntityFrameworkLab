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

namespace EFLab.DAL
{
    public class DbEntities : DbContext
    {
        public DbEntities()
            : base("name=EFLab")
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            //Database.SetInitializer<DbEntities>(new DbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<SecondLevelObjectBase>().ToTable("t_SecondLevelObjectBase", "Core");
            //modelBuilder.Entity<EFLab.DAL.BizObjects.TypeA.TypeASecondLevelObject>().ToTable("t_TypeASecondLevel", "TypeA");
            //modelBuilder.Entity<EFLab.DAL.BizObjects.TypeB.TypeBSecondLevelObject>().ToTable("t_TypeBSecondLevel", "TypeB");
            //modelBuilder.Entity<EFLab.DAL.BizObjects.TypeC.TypeCSecondLevelObject>().ToTable("t_TypeCSecondLevel", "TypeC");

            //modelBuilder.Entity<SecondLevelObjectBase>()
            //    .Property(c => c.SecondLevelObjectBaseId)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //modelBuilder.Entity<TypeASecondLevelObject>().Map(m =>
            //{
            //    m.MapInheritedProperties();
            //    m.ToTable("t_SecondLevelObjectA", "TypeA");
            //});

            //modelBuilder.Entity<TypeBSecondLevelObject>().Map(m =>
            //{
            //    m.MapInheritedProperties();
            //    m.ToTable("t_SecondLevelObjectB", "TypeB");
            //});

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<TopLevelObject> TopLevelObject { get; set; }
        public virtual DbSet<SecondLevelObjectBase> SecondLevelObject { get; set; }
        public virtual DbSet<EFLab.DAL.BizObjects.TypeA.TypeASecondLevelObject> TypeASecondLevelObject { get; set; }
        public virtual DbSet<EFLab.DAL.BizObjects.TypeB.TypeBSecondLevelObject> TypeBSecondLevelObject { get; set; }
        public virtual DbSet<EFLab.DAL.BizObjects.TypeC.TypeCSecondLevelObject> TypeCSecondLevelObject { get; set; }
    }
}
