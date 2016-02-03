using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using EFLab.DAL;
using EFLab.DAL.BizObjects;
using EFLab.DAL.BizObjects.TypeA;
using EFLab.DAL.BizObjects.TypeB;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace EFLab.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (DbEntities context = new DbEntities())
            {
                TopLevelObject top = new TopLevelObject();
                context.TopLevelObject.Add(top);
                context.SaveChanges();

                IEnumerable<SecondLevelObjectBase> objects =
                    (from t in context.TopLevelObject
                     join s in context.SecondLevelObject
                     on t.TopLevelObjectId equals s.Parent.TopLevelObjectId
                     where t.TopLevelObjectId == 1
                     select s).ToList();

                Assert.AreEqual(objects.Count(), 2);

                IEnumerable<TypeASecondLevelObject> typeAObjects = objects.OfType<TypeASecondLevelObject>();

                Assert.AreEqual(typeAObjects.Count(), 1);

                IEnumerable<TypeBSecondLevelObject> typeBObjects = objects.OfType<TypeBSecondLevelObject>();

                Assert.AreEqual(typeBObjects.Count(), 1);

                //foreach (SecondLevelObjectBase o in objects)
                //{
                //    Debug.WriteLine(o.GetType());
                //}

                //TopLevelObject top2 =
                //    (from p in context.TopLevelObject
                //     where p.TopLevelObjectId == 1
                //     select p)
                //    .FirstOrDefault();
            }
        }
    }
}
