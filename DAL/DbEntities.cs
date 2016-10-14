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

            /*
             System.Data.Entity.DbModelBuilder
             public virtual EntityTypeConfiguration<TEntityType> DbModelBuilder.Entity<TEntityType>()
             where TEntityType : class
            
             Registers an entity type as part of the model and returns an object that can be used to configure the entity.
             This method can be called multiple times for the same entity to perform multiple lines of configuration.
            
             https://aleemkhan.wordpress.com/2013/02/28/dynamically-adding-dbset-properties-in-dbcontext-for-entity-framework-code-first/
             http://stackoverflow.com/questions/14843462/how-can-i-use-reflection-to-invoke-dbmodelbuilder-entityt-ignorex-x-proper
             http://stackoverflow.com/questions/15481685/querying-against-dbcontext-settypevariable-in-entity-framework
             http://stackoverflow.com/questions/19644617/linq-multiple-join-iqueryable-modify-result-selector-expression
             
            */
            MethodInfo method = modelBuilder.GetType().GetMethod("Entity");
            method = method.MakeGenericMethod(new Type[] { typeof(EFLab.DAL.BizObjects.TypeA.TypeAObject1) });
            method.Invoke(modelBuilder, null);

            //this.AddDbSet(modelBuilder, typeof(EFLab.DAL.BizObjects.TypeA.TypeAObject1));
            //this.AddDbSet(modelBuilder, typeof(EFLab.DAL.BizObjects.TypeB.TypeBObject1));
            //this.AddDbSet(modelBuilder, typeof(EFLab.DAL.BizObjects.TypeC.TypeCObject1));

            //EntityTypeConfiguration<TopLevelObject> config = modelBuilder.Entity<TopLevelObject>();

            base.OnModelCreating(modelBuilder);
        }

        private void AddDbSet(DbModelBuilder modelBuilder, Type type)
        {
            MethodInfo method = modelBuilder.GetType().GetMethod("Entity");
            method = method.MakeGenericMethod(new Type[] { type });
            method.Invoke(modelBuilder, null);
        }

        public virtual DbSet<TopLevelObject> TopLevelObject { get; set; }
        public virtual DbSet<SecondLevelObjectBase> SecondLevelObject { get; set; }
        public virtual DbSet<EFLab.DAL.BizObjects.TypeA.TypeASecondLevelObject> TypeASecondLevelObject { get; set; }
        public virtual DbSet<EFLab.DAL.BizObjects.TypeB.TypeBSecondLevelObject> TypeBSecondLevelObject { get; set; }
        public virtual DbSet<EFLab.DAL.BizObjects.TypeC.TypeCSecondLevelObject> TypeCSecondLevelObject { get; set; }

        public virtual DbSet<SecondLevelObjectBase> GetSecondLevelObject()
        {
            return (DbSet<SecondLevelObjectBase>)this.Set<SecondLevelObjectBase>();
        }
    }

    //internal sealed class MyConfiguration : DbMigrationsConfiguration<DbEntities>
    //{
    //    public MyConfiguration()
    //    {
    //        AutomaticMigrationsEnabled = false;
    //    }

    //    protected override void Seed(DbEntities context)
    //    {
    //        // TODO: Initialize seed data here
    //    }
    //}
}
