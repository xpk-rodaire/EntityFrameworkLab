Table-per-Hierarchy (TPH)
http://www.dotnet-tricks.com/Tutorial/entityframework/KP45031213-Understanding-Inheritance-in-Entity-Framework.html
http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/implementing-inheritance-with-the-entity-framework-in-an-asp-net-mvc-application

By default, Entity Framework supports TPH inheritance, if you don't define any mapping details for your inheritance hierarchy.
The TPC inheritance is commonly used to inherit basic features.
TPH inheritance patterns generally deliver better performance in the Entity Framework than TPT inheritance patterns, because TPT patterns can result in complex join queries.

TopLevelObject
   public virtual IList<SecondLevelObjectBase> SecondLevelObjects { get; set; }

   SecondLevelObjectBase
      TypeASecondLevelObject
	     public virtual IList<TypeAObject1> TypeAObject1s { get; set; }

	  TypeBSecondLevelObject
	     public virtual IList<TypeBObject1> TypeBObject1s { get; set; }

	  TypeCSecondLevelObject
	     public virtual IList<TypeCObject1> TypeCObject1s { get; set; }

Diff between Database project and contents of database

ISD40581\SQL2012.EFLab
EFLab.Database
EFLab.DAL

1) Sync EFLab.DAL to ISD40581\SQL2012.EFLab
2) Import ISD40581\SQL2012.EFLab to EFLab.Database
3) Make change to object(s) in EFLab.DAL
4) Add-Migration
5) Update-Database
6) Generate DACPAC file from EFLab.Database
7) Schema compare EFLab.DAL and EFLab.Database DACPAC file
8) Generate SQL script


Biz Objects - divide by namespace (Transmission, TransmissionSet, etc.)
DBSets - 
Database
DAL

DbSet set = context.Set(
    typeof( MyEntity )
);

[BizObjectType("Transmission")]
[TaxYear("2015")]

public List<> GetTransmissions(string taxYear)
{
    using (DbEntities context = new DbEntities())
    {

	    context.SecondLevelObject.OfType<T>()

        IList<T> objects =
            (from t in context.TopLevelObject
                join s in context.SecondLevelObject.OfType<T>()
                on t.TopLevelObjectId equals s.Parent.TopLevelObjectId
                where t.TopLevelObjectId == topLevelId
                select s)
                .ToList();

        return objects;
    }
}


DBContext per tax year
DAL will choose DBContext based on year!

namespace System.Data.Entity
{
  public static class EntityFrameworkExtensions
  {
    public static IEnumerable<object> AsEnumerable(this DbSet set)
    {
      foreach (var entity in set)
        yield return entity;
    }
  }
}

DbContext has a method called Set, that you can use to get a non-generic DbSet, such as:

var someDbSet = this.Set(typeof(SomeEntity));
So in your case:

foreach (BaseEntity entity in list)
{
      cntx.Set(entity.GetType()).Add(entity);         
}

https://aleemkhan.wordpress.com/2013/02/28/dynamically-adding-dbset-properties-in-dbcontext-for-entity-framework-code-first/

protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            CustomAssemblySection configSection = (CustomAssemblySection)System.Configuration.ConfigurationManager.GetSection("CustomAssemblySection");
 

            foreach (CustomAssembly customAssembly in configSection.Assemblies)
            {
                Assembly assembly = Assembly.Load(customAssembly.Name);
                foreach (Type type in assembly.ExportedTypes)
                {
                    if (type.IsClass)
                    {
                        MethodInfo method = modelBuilder.GetType().GetMethod("Entity");
                        method = method.MakeGenericMethod(new Type[] { type });
                        method.Invoke(modelBuilder, null);
                    }
                }
            }
            base.OnModelCreating(modelBuilder);
        }

https://romiller.com/2012/03/26/dynamically-building-a-model-with-code-first/


ObjectType AmountByMonthDetailType
TaxYear    2015

Put each TY in own namespace/database schema

Put each TY in own database
   Take some off-line for security?
   How would security key work?

Put all TY in same database, each TY in own DBContext
   Have a separate DAL for each DBContext - lot of code duplication!
   Have Transmission object for each TY?

public List<SCO.IRS.ACA.FormData.DAL.TY2015_06.Form1094CUpstreamDetailType>
Transmission.Form1094CRecords  { get; set; }

Need abstract/base class Form1094CUpstreamDetailType