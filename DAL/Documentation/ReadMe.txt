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




