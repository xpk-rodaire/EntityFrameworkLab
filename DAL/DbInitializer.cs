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
            top.PopulateTest();

            context.TopLevelObject.Add(top);

            base.Seed(context);
        }
    }
}
