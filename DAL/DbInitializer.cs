using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

using EFLab.DAL.BizObjects;
using EFLab.DAL.BizObjects.TypeA;
using EFLab.DAL.BizObjects.TypeB;

namespace EFLab.DAL
{
    class DbInitializer : DropCreateDatabaseAlways<DbEntities>
    {

        protected override void Seed(DbEntities context)
        {
            TopLevelObject top = new TopLevelObject();

            EFLab.DAL.BizObjects.TypeA.TypeASecondLevelObject objectA = new EFLab.DAL.BizObjects.TypeA.TypeASecondLevelObject();
            objectA.Property1 = "Property1";
            objectA.Property2 = "Property2";
            objectA.Property3 = "Property3";
            objectA.Property4 = "Property4";
            objectA.Property5 = "Property5";
            objectA.Property6 = "Property6";

            top.SecondLevelObjects.Add(objectA);

            EFLab.DAL.BizObjects.TypeB.TypeBSecondLevelObject objectB = new EFLab.DAL.BizObjects.TypeB.TypeBSecondLevelObject();
            objectB.Property1 = "Property1";
            objectB.Property2 = "Property2";
            objectB.Property3 = "Property3";
            objectB.Property4 = "Property4";
            objectB.Property5 = "Property5";
            objectB.Property6 = "Property6";

            top.SecondLevelObjects.Add(objectB);

            context.TopLevelObject.Add(top);

            base.Seed(context);
        }
    }
}
